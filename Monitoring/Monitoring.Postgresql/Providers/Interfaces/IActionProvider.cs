using Monitoring.Postgresql.Models.Action;

namespace Monitoring.Postgresql.Providers.Interfaces;

public interface IActionProvider
{
    Task<IEnumerable<ActionDTO>> GetAllActionsAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<ActionDTO>> GetUserActionsAsync(int telegramBotUserId, CancellationToken cancellationToken = default);
    Task UpdateTelegramBotUserActionsAsync(UpdateUserActionsDTO dto, CancellationToken cancellationToken = default);
}