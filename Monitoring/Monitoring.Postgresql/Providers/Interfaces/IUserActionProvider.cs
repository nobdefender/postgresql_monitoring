using Monitoring.Postgresql.Models;

namespace Monitoring.Postgresql.Providers.Implementations;

public interface IUserActionProvider
{
    Task Save(ZabbixRequestModel zabbixRequestModel, CancellationToken cancellationToken);

    Task<bool> CheckSelect(UserActionRequestModel userActionRequestModel, CancellationToken cancellationToken);

    Task SetSelect(string callbackData, CancellationToken cancellationToken);

}