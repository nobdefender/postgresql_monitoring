using Microsoft.EntityFrameworkCore;
using Monitoring.Posgresql.Infrastructure;

namespace Monitoring.Postgresql.Logic.Registars;

public static class DbRegistar
{
    /// <summary>
    /// Регистрирует ДБ контекст.
    /// </summary>
    /// <param name="services">Коллекция дескрипторов сервисов.</param>
    public static void RegisterDbServices(this IServiceCollection services)
    {
        services.AddDbContext<MonitoringServiceDbContext>(options =>
        {
            options.UseNpgsql("name=ConnectionStrings:MonitoringServiceConnectionString",
                b => b.MigrationsAssembly("Monitoring.Postgresql"));
            options.EnableSensitiveDataLogging(true);
        });
    }
}