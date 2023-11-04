using Microsoft.AspNetCore.Mvc;
using Monitoring.Postgresql.Models;
using Monitoring.Postgresql.Providers.Implementations;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Monitoring.Postgresql.Controllers;

public class UserActionController : BaseController
{
    private readonly UserActionProvider _userActionProvider;

    //TODO: add Interface
    public UserActionController(ILogger<UserActionController> logger, UserActionProvider userActionProvider) :
        base(logger)
    {
        _userActionProvider = userActionProvider;
    }

    [SwaggerRequestExample(typeof(ZabbixRequestModel), typeof(ZabbixRequestModel))]
    [Route("api/UserAction/Push")]
    [HttpPost]
    public async Task<IActionResult> PushUserAction([FromBody] ZabbixRequestModel zabbixRequestModel,
        CancellationToken cancellationToken)
    {
        return await Execute(_userActionProvider.SaveUserAction(zabbixRequestModel, cancellationToken));
    }

    [Route("api/UserAction/Get")]
    [HttpPost]
    public async Task GetUserAction([FromBody] UserActionRequestModel userActionModel,
        CancellationToken cancellationToken)
    {
        await Execute(async () => await _userActionProvider.GetUserAction(userActionModel, cancellationToken));
    }
}