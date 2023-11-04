//using Monitoring.Posgresql.Infrastructure;
//using Monitoring.Postgresql.Models;
//using System.Net.Sockets;
//using Telegram.Bot;
//using Telegram.Bot.Types.ReplyMarkups;

//namespace Monitoring.Postgresql.Providers.Implementations;

//public class BotProvider : IBotProvider
//{

//    private readonly TelegramBotClient _telegramBotClient;
//    private readonly MonitoringServiceDbContext _monitoringServiceDbContext;

//    public BotProvider(TelegramBotClient telegramBotClient, MonitoringServiceDbContext monitoringServiceDbContext)
//    {
//        _telegramBotClient = telegramBotClient;
//        _monitoringServiceDbContext = monitoringServiceDbContext;
//    }

//    public async Task SelectUserAction(string callbackData, CancellationToken cancellationToken)
//    {
//        var userActionHash = long.Parse(callbackData.Split(' ').Last());



//    }

//    public async Task SendUserActionMessage(UserActionDbModel[] userActionDbModels, string message, CancellationToken cancellationToken)
//    {
//        //var userActionName = userActionDbModels.Select(x => x.ActionName);

//        //var actions = _monitoringServiceDbContext.Actions.Where(x => userActionName.Contains(x.Name));

//        //if (!actions.Any())
//        //{
//        //    return;
//        //}

//        long[] chatIds = new[] { 1458662165l };

//        var userActionButtons = userActionDbModels.Select(x => new[] { InlineKeyboardButton.WithCallbackData(x.ButtonName, $"UserAction_{x.Hash}") });

//        await Task.WhenAll(chatIds.Select(x =>
//            _telegramBotClient.SendTextMessageAsync(new Telegram.Bot.Types.ChatId(x), message, replyMarkup: new InlineKeyboardMarkup(userActionButtons))
//        ));

//    }

//}