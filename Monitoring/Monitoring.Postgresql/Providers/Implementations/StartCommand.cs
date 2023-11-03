using Telegram.Bot.Types;
using Telegram.Bot;
using Monitoring.Postgresql.Providers.Interfaces;

namespace Monitoring.Postgresql.Providers.Implementations;

public class StartCommand : ICommand
{
    public TelegramBotClient Client => TelegramBotProvider.GetBot();

    public string Name => "/start";

    public async Task Execute(Update update)
    {
        var chatId = update.Message.Chat.Id;
        await Client.SendTextMessageAsync(chatId, "Привет!");
    }
}