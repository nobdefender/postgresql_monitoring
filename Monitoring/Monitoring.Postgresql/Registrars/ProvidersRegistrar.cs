using Monitoring.Postgresql.Options;
using Monitoring.Postgresql.Providers.Implementations;
using Telegram.Bot;

namespace Monitoring.Postgresql.Registrars;

public static class ProvidersRegistrar
{
    public static IServiceCollection RegisterProvider(this IServiceCollection services, IConfiguration configuration)
    {
        var appSettingsOptions = configuration.Get<AppSettingsOptions>();
        services.AddSingleton(x => new TelegramBotClient(appSettingsOptions.AppSettings.TelegramBotToken));

        services.AddTransient<IUserActionProvider, UserActionProvider>();

        return services;
    }
}