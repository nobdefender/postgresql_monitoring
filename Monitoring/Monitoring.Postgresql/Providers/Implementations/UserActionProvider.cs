using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Core.WireProtocol.Messages;
using Monitoring.Posgresql.Infrastructure.Extensions;
using Monitoring.Posgresql.Infrastructure.Mongo;
using Monitoring.Postgresql.Models;
using Monitoring.Postgresql.Options;
using Newtonsoft.Json.Linq;

namespace Monitoring.Postgresql.Providers.Implementations;

public class UserActionProvider : IUserActionProvider
{
    private IMongoCollection<UserActionDbModel> _userActionCollection;

    public UserActionProvider(IMongoFactory mongoFactory, IOptions<UserActionOptions> userActionOptions)
    {
        var db = mongoFactory.GetDatabase(userActionOptions.Value.UserAction.MongoConnectionString);
        _userActionCollection = db.GetCollection<UserActionDbModel>("UserActionModel");
    }

    private static long GetHash(UserActionDbModel userActionModel)
    {
        string fullStr = (userActionModel.ActionName.ToString())
            //+ (userActionModel.ButtonName.ToString())
            + (string.Join("", userActionModel.ActionParams))
            ;

        return fullStr.CalculateHash();
    }

    public async Task SaveUserAction(ZabbixRequestModel zabbixRequestModel, CancellationToken cancellationToken)
    {
        // TODO: add map
        var test = new UserActionDbModel[] { };

        foreach (var item in test)
        {
            item.Hash = GetHash(item);
        }

        var upsertOperations = test.Select(x => new ReplaceOneModel<UserActionDbModel>(
                        Builders<UserActionDbModel>.Filter.Eq(y => y.Hash, x.Hash),
                        x
                        )
        {
            IsUpsert = true
        });

        await _userActionCollection.BulkWriteAsync(upsertOperations);

        //await _userActionCollection.UpdateManyAsync()// ReplaceAsync(x => x.Hash == test.Hash, test, new ReplaceOptions() { IsUpsert = true });

    }

    public async Task<bool> GetUserAction(UserActionRequestModel userActionDbModel, CancellationToken cancellationToken)
    {
        var hash = GetHash(userActionDbModel);

        var selectedUserActions = await _userActionCollection.Find(Builders<UserActionDbModel>.Filter.And(
            Builders<UserActionDbModel>.Filter.Eq(x => x.Hash, hash),
            Builders<UserActionDbModel>.Filter.Eq(x => x.IsSelected, true))
            ).FirstOrDefaultAsync(cancellationToken);

        if (selectedUserActions == null)
        {
            return false;
        }

        await _userActionCollection.DeleteManyAsync(Builders<UserActionDbModel>.Filter.Eq(x => x.Hash, selectedUserActions.Hash));

        return true;
    }
}