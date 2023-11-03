using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Monitoring.Posgresql.Infrastructure.Mongo;

public class MongoFactory : IMongoFactory
{
    private Dictionary<string, IMongoClient> _clientScope = new Dictionary<string, IMongoClient>();

    public IMongoDatabase GetDatabase(string connectionString)
    {
        if (string.IsNullOrEmpty(connectionString))
            throw new Exception("connectionString is empty");

        var mongoUrl = MongoUrl.Create(connectionString);

        if (string.IsNullOrEmpty(mongoUrl.DatabaseName))
            throw new Exception("DatabaseName is empty");

        return GetClient(mongoUrl).GetDatabase(mongoUrl.DatabaseName);
    }

    private IMongoClient GetClient(MongoUrl url)
    {
        string key = string.Join(",", url.Servers);

        if (!_clientScope.ContainsKey(key))
        {
            lock (_clientScope)
            {
                if (!_clientScope.ContainsKey(key))
                {
                    var settings = MongoClientSettings.FromConnectionString(url.Url);
                    settings.LinqProvider = LinqProvider.V2;

                    _clientScope.Add(key, new MongoClient(settings));
                }
            }
        }

        return _clientScope[key];
    }
}