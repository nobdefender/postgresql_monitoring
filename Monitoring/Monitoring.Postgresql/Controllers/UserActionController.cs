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
    public UserActionController(ILogger<UserActionController> logger, UserActionProvider userActionProvider) : base(logger)
    {
        _userActionProvider = userActionProvider;
    }

    [SwaggerRequestExample(typeof(ZabbixRequestModel), typeof(ZabbixRequestModel))]
    [Route("api/UserAction/Push")]
    [HttpPost]
    public async Task<IActionResult> PushUserAction([FromBody] ZabbixRequestModel zabbixRequestModel, CancellationToken cancellationToken)
    {
        return await Execute(_userActionProvider.Save(zabbixRequestModel, cancellationToken));
    }


    [SwaggerResponse(200, Type = typeof(bool))]
    [Route("api/UserAction/CheckSelect")]
    [HttpPost]
    public async Task<IActionResult> GetUserAction([FromBody] UserActionRequestModel userActionModel, CancellationToken cancellationToken)
    {
        return await Execute(async () => await _userActionProvider.GetSelect(userActionModel, cancellationToken));
    }
}