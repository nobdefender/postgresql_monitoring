using Telegram.Bot;

namespace Monitoring.Postgresql.Providers.Implementations;

public class TelegramBotProvider
{
    private static TelegramBotClient _client;

    public static TelegramBotClient GetBot()
    {
        if (_client != null)
        {
            return _client;
        }

        _client = new TelegramBotClient("6896358943:AAEAVqGmME25pHXK76Ed2kp0hCA61AGOtoM");

        return _client;
    }

}