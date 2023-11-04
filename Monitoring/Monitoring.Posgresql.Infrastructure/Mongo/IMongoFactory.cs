using MongoDB.Driver;

namespace Monitoring.Posgresql.Infrastructure.Mongo;

public interface IMongoFactory
{
    IMongoDatabase GetDatabase(string connectionString);
}