using Monitoring.Postgresql.Options;

namespace Monitoring.Postgresql.Registrars;

public static class OptionsRegistrar
{
    public static IServiceCollection RegisterOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AppSettingsOptions>(configuration);
        services.Configure<UserActionOptions>(configuration);

        return services;
    }
}
