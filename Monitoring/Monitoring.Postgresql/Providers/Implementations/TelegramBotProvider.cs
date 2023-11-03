using Microsoft.Extensions.Options;
using Monitoring.Postgresql.Options;
using Telegram.Bot;

namespace Monitoring.Postgresql.Providers.Implementations;

public class TelegramBotProvider
{
    private readonly AppSettings _appSettings;
    private TelegramBotClient _client;

    public TelegramBotProvider(IOptions<AppSettingsOptions> appSettingsOptions)
    {
        _appSettings = appSettingsOptions.Value.AppSettings;
    }

    public TelegramBotClient GetBot()
    {
        if (_client != null)
        {
            return _client;
        }

        _client = new TelegramBotClient(_appSettings.TelegramBotToken);

        return _client;
    }
}