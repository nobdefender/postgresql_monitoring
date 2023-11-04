using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Monitoring.Posgresql.Infrastructure.Mongo;

namespace Monitoring.Posgresql.Infrastructure.Extensions;

public static class ServiceCollectionMongoExtention
{
    public static IServiceCollection RegisterMongo(this IServiceCollection services)
    {
        services.TryAddSingleton<IMongoFactory, MongoFactory>();

        return services;
    }
}