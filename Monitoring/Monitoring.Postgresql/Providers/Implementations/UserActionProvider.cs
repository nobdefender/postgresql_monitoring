using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Monitoring.Posgresql.Infrastructure.Extensions;
using Monitoring.Posgresql.Infrastructure.Mongo;
using Monitoring.Postgresql.Models;
using Monitoring.Postgresql.Options;

namespace Monitoring.Postgresql.Providers.Implementations;

public class UserActionProvider : IUserActionProvider
{
    //TODO: add interface

    private readonly IMongoCollection<UserActionDbModel> _userActionCollection;
    private readonly BotProvider _botProvider;
    private readonly IMapper _mapper;

    public UserActionProvider(IMongoFactory mongoFactory, IOptions<UserActionOptions> userActionOptions, IMapper mapper,
        BotProvider botProvider)
    {
        var db = mongoFactory.GetDatabase(userActionOptions.Value.UserAction.MongoConnectionString);
        _userActionCollection = db.GetCollection<UserActionDbModel>("UserActionModel");

        _mapper = mapper;
        _botProvider = botProvider;
    }

    private static long GetHash(UserActionDbModel userActionModel)
    {
        string fullStr = (userActionModel.ActionName.ToString())
            //+ (userActionModel.ButtonName.ToString())
            + (string.Join("", userActionModel.ActionParams))
            ;

        return fullStr.CalculateHash();
    }

    public async Task SaveUserAction(ZabbixRequestModel zabbixRequestModel, CancellationToken cancellationToken)
    {
        var userActionDbModels = _mapper.Map<UserActionDbModel[]>(zabbixRequestModel.userActionModels);

        foreach (var userActionDbModelItem in userActionDbModels)
        {
            userActionDbModelItem.Hash = GetHash(userActionDbModelItem);
        }

        var upsertOperations = userActionDbModels.Select(x => new ReplaceOneModel<UserActionDbModel>(
                        Builders<UserActionDbModel>.Filter.Eq(y => y.Hash, x.Hash),
                        x
                        )
        {
            IsUpsert = true
        });

        await _userActionCollection.BulkWriteAsync(upsertOperations, cancellationToken: cancellationToken);

        await _botProvider.SendUserActionMessage(userActionDbModels, cancellationToken);
    }

    public async Task<bool> GetUserAction(UserActionRequestModel userActionRequestModel, CancellationToken cancellationToken)
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
}