using Monitoring.Postgresql.Providers.Implementations;

namespace Monitoring.Postgresql.Registrars;

public static class ProvidersRegistrar
{
    public static IServiceCollection RegisterProvider(this IServiceCollection services)
    {
        services.AddSingleton<TelegramBotProvider>();

        return services;
    }
}