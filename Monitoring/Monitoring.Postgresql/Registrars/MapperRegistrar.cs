﻿using Monitoring.Postgresql.Mappers;

namespace Monitoring.Postgresql.Registrars;

public static class MapperRegistrar
{
    /// <summary>
    /// Регистрирует маппинги.
    /// </summary>
    /// <param name="services">Коллекция дескрипторов сервисов.</param>
    public static IServiceCollection RegisterMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(srv => { srv.AddProfile<DbModelsToDTOModels>(); });

        return services;
    }
}