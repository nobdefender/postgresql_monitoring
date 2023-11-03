using Monitoring.Postgresql.Models;
using System.Net.Sockets;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace Monitoring.Postgresql.Providers.Implementations;

public class BotProvider : IBotProvider
{

    private readonly TelegramBotClient _telegramBotClient;

    public BotProvider(TelegramBotClient telegramBotClient)
    {
        _telegramBotClient = telegramBotClient;
    }

    public async Task SendUserActionMessage(UserActionDbModel[] userActionDbModels)
    {
        long[] chatIds = new[] { 1l};

        var userActionButtons = userActionDbModels.Select(x => new[] { InlineKeyboardButton.WithCallbackData(x.ButtonName, $"UserAction_{x.Hash}") });
       
        foreach (var chatItem in chatIds)
        {
            await _telegramBotClient.SendTextMessageAsync(new Telegram.Bot.Types.ChatId(chatItem) , "", replyMarkup: new InlineKeyboardMarkup(userActionButtons) );
        }

    }
}