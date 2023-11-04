using Monitoring.Posgresql.Infrastructure.Models.TelegramBot;

namespace Monitoring.Postgresql.Providers.Interfaces;

public interface ITelegramBotUserProvider
{
    Task Save(TelegramBotUserDbModel telegramBotUserDbModel, CancellationToken cancellationToken);
    Task<IEnumerable<TelegramBotUserDbModel>> GetAllTelegramBotUsersAsync(CancellationToken cancellationToken);
}