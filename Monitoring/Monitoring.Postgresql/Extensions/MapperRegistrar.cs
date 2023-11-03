using Monitoring.Postgresql.Mappers;

namespace Monitoring.Postgresql.Extensions;

public static class MapperRegistrar
{
    /// <summary>
    /// Регистрирует маппинги.
    /// </summary>
    /// <param name="services">Коллекция дескрипторов сервисов.</param>
    public static void RegisterMapperServices(this IServiceCollection services)
    {
        services.AddAutoMapper(srv => { srv.AddProfile<DbModelsToDTOModels>(); });
    }
}