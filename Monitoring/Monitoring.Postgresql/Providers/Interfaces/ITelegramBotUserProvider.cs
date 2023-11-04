using Monitoring.Posgresql.Infrastructure.Models.TelegramBot;

namespace Monitoring.Postgresql.Providers.Interfaces;

public interface ITelegramBotUserProvider
{
    Task<IEnumerable<TelegramBotUserDbModel>> GetAllTelegramBotUsersAsync(CancellationToken cancellationToken);
}