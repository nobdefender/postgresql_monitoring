using Monitoring.Postgresql.Mappings;

namespace Monitoring.Postgresql.Registrars;

public static class MapperRegistrar
{
    /// <summary>
    /// Регистрирует маппинги.
    /// </summary>
    /// <param name="services">Коллекция дескрипторов сервисов.</param>
    public static IServiceCollection RegisterMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(srv =>
        {
            srv.AddProfile<DbModelsToDTOModelsMapping>();
            srv.AddProfile<UserActionRequestModelToUserActionDbModelMapping>();
        });

        return services;
    }
}
