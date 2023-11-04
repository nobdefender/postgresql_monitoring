using AutoMapper;
using Monitoring.Posgresql.Infrastructure;
using Monitoring.Posgresql.Infrastructure.Models.Bindings;
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
        var actionIds = _monitoringServiceDbContext.UserToAction
            .Where(x => x.UserId == userId && !x.IsDeleted.HasValue).Select(x => x.ActiondId).ToList();
        var actions = _monitoringServiceDbContext.Actions.Where(x => actionIds.Contains(x.Actionid)).ToList();
        return Task.FromResult(actions.Select(_mapper.Map<ActionDTO>));
    }

    public async Task UpdateUserActionsAsync(UpdateUserActionsDTO dto, CancellationToken cancellationToken)
    {
        var actions = _monitoringServiceDbContext.UserToAction
            .Where(x => x.UserId == dto.UserId && !x.IsDeleted.HasValue).ToList();
        foreach (var action in actions)
        {
            action.IsDeleted = true;
        }

        foreach (var id in dto.ActionIds)
        {
            await _monitoringServiceDbContext.UserToAction.AddAsync(new UserToActionDbModel()
            {
                ActiondId = id,
                UserId = dto.UserId
            }, cancellationToken);
        }

        await _monitoringServiceDbContext.SaveChangesAsync(cancellationToken);
    }
}