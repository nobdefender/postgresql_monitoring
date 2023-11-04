using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Monitoring.Posgresql.Infrastructure;
using Monitoring.Posgresql.Infrastructure.Extensions;
using Monitoring.Posgresql.Infrastructure.Mongo;
using Monitoring.Postgresql.Models;
using Monitoring.Postgresql.Options;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace Monitoring.Postgresql.Providers.Implementations;

public class UserActionProvider : IUserActionProvider
{
    //TODO: add interface

    private readonly IMongoCollection<UserActionDbModel> _userActionCollection;
    private readonly MonitoringServiceDbContext _monitoringServiceDbContext;
    private readonly TelegramBotClient _telegramBotClient;
    private readonly IMapper _mapper;

    private static SemaphoreSlim _lockGetSelect = new SemaphoreSlim(1, 1);

    public UserActionProvider(IMongoFactory mongoFactory, IOptions<UserActionOptions> userActionOptions, IMapper mapper,
        MonitoringServiceDbContext monitoringServiceDbContext, TelegramBotClient telegramBotClient)
    {
        var db = mongoFactory.GetDatabase(userActionOptions.Value.UserAction.MongoConnectionString);
        _userActionCollection = db.GetCollection<UserActionDbModel>("UserActionModel");

        _mapper = mapper;
        _monitoringServiceDbContext = monitoringServiceDbContext;
        _telegramBotClient = telegramBotClient;
    }

    public async Task Save(ZabbixRequestModel zabbixRequestModel, CancellationToken cancellationToken)
    {
        var userActionDbModels = _mapper.Map<UserActionDbModel[]>(zabbixRequestModel.userActionModels);

        foreach (var userActionDbModelItem in userActionDbModels)
        {
            userActionDbModelItem.Hash = GetHash(userActionDbModelItem);
        }

        var upsertOperations = userActionDbModels.Select(x => new ReplaceOneModel<UserActionDbModel>(
            Builders<UserActionDbModel>.Filter.Eq(y => y.Hash, x.Hash), x)
        {
            IsUpsert = true
        });

        await _userActionCollection.BulkWriteAsync(upsertOperations, cancellationToken: cancellationToken);

        await SendMessage(userActionDbModels, zabbixRequestModel.Message, cancellationToken);
    }

    public async Task<bool> CheckSelect(UserActionRequestModel userActionRequestModel, CancellationToken cancellationToken)
    {
        await _lockGetSelect.WaitAsync();
        try
        {

            var userActionDbModel = _mapper.Map<UserActionDbModel>(userActionRequestModel);

            var hash = GetHash(userActionDbModel);

            var selectedUserActions = await _userActionCollection.Find(Builders<UserActionDbModel>.Filter.And(
                Builders<UserActionDbModel>.Filter.Eq(x => x.Hash, hash),
                Builders<UserActionDbModel>.Filter.Eq(x => x.IsSelected, true))
                ).FirstOrDefaultAsync(cancellationToken);

            if (selectedUserActions == null)
            {
                return false;
            }

            await _userActionCollection.DeleteManyAsync(Builders<UserActionDbModel>.Filter.Eq(x => x.Hash, selectedUserActions.Hash));

            return true;
        }
        finally
        {
            _lockGetSelect.Release();
        }
    }

    public async Task SetSelect(string callbackData, CancellationToken cancellationToken)
    {
        var userActionHash = long.Parse(callbackData.Split('_').Last());

        await _userActionCollection.UpdateOneAsync(x => x.Hash == userActionHash,
            Builders<UserActionDbModel>.Update.Set(y => y.IsSelected, true),
            cancellationToken: cancellationToken);
    }

    private async Task SendMessage(UserActionDbModel[] userActionDbModels, string message, CancellationToken cancellationToken)
    {
        var userActionNames = userActionDbModels.Select(x => x.ActionName);

        var userToActionByUserId = await _monitoringServiceDbContext.UserToAction
            .AsNoTracking()
            .Where(x => userActionNames.Contains(x.ActionDbModel.Name))
            .GroupBy(x => x.UserDbModel.TelegramChatId)
            .ToArrayAsync(cancellationToken);

        foreach (var userToActionItem in userToActionByUserId)
        {
            var currentUserActionNames = userToActionItem.Select(x => x.UserDbModel.Name);
            var currentActions = userActionDbModels.Where(x => currentUserActionNames.Contains(x.ActionName));

            if (!currentActions.Any())
            {
                continue;
            }

            var userActionButtons = currentActions.Select(x => new[] { InlineKeyboardButton.WithCallbackData(x.ButtonName, $"UserAction_{x.Hash}") });

            await _telegramBotClient.SendTextMessageAsync(new Telegram.Bot.Types.ChatId(userToActionItem.Key), message, replyMarkup: new InlineKeyboardMarkup(userActionButtons), cancellationToken: cancellationToken);
        }
    }

    private static long GetHash(UserActionDbModel userActionModel)
    {
        string fullStr = (userActionModel.ActionName.ToString())
            //+ (userActionModel.ButtonName.ToString())
            + (string.Join("", userActionModel.ActionParams))
            ;

        return fullStr.CalculateHash();
    }
}