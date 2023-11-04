using Monitoring.Posgresql.Infrastructure.Models.Access;
using Monitoring.Postgresql.Models.Action;

namespace Monitoring.Postgresql.Providers.Interfaces;

public interface IActionProvider
{
    Task<IEnumerable<ActionDTO>> GetAllActionsAsync(CancellationToken cancellationToken = default);
    Task<ActionDTO> GetActionByIdAsync(int? id, CancellationToken cancellationToken = default);
    Task CreateActionAsync(ActionDTO dto, CancellationToken cancellationToken = default);
    Task UpdateActionAsync(ActionDbModel model, CancellationToken cancellationToken = default);
    Task DeleteActionAsync(int id, CancellationToken cancellationToken = default);

    Task<IEnumerable<ActionDTO>> GetUserActionsAsync(int telegramBotUserId,
        CancellationToken cancellationToken = default);

    Task UpdateTelegramBotUserActionsAsync(UpdateUserActionsDTO dto, CancellationToken cancellationToken = default);
}