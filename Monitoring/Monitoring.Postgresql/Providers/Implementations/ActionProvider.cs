using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Monitoring.Posgresql.Infrastructure;
using Monitoring.Postgresql.Models.Action;
using Monitoring.Postgresql.Providers.Interfaces;

namespace Monitoring.Postgresql.Providers.Implementations;

public class ActionProvider : IActionProvider
{
    private readonly IMapper _mapper;
    private readonly MonitoringServiceDbContext _monitoringServiceDbContext;

    public ActionProvider(MonitoringServiceDbContext monitoringServiceDbContext, IMapper mapper)
    {
        _mapper = mapper;
        _monitoringServiceDbContext = monitoringServiceDbContext;
    }

    public Task<IEnumerable<ActionDTO>> GetAllActionsAsync(CancellationToken cancellationToken)
    {
        var actions = _monitoringServiceDbContext.Actions;
        return Task.FromResult(actions.Select(_mapper.Map<ActionDTO>));
    }

    public Task<IEnumerable<ActionDTO>> GetUserActionsAsync(int userId, CancellationToken cancellationToken)
    {
        var actionsId = _monitoringServiceDbContext.UserToAction
            .Where(x => x.UserId == userId && !x.IsDeleted.HasValue).Select(x => x.ActiondId).ToList();
        var actions = _monitoringServiceDbContext.Actions.Where(x => actionsId.Contains(x.Actionid)).ToList();
        return Task.FromResult(actions.Select(_mapper.Map<ActionDTO>));
    }
}