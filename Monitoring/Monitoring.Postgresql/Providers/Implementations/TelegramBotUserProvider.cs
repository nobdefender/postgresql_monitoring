using Microsoft.EntityFrameworkCore;
using Monitoring.Posgresql.Infrastructure;
using Monitoring.Posgresql.Infrastructure.Models.TelegramBot;
using Monitoring.Postgresql.Providers.Interfaces;

namespace Monitoring.Postgresql.Providers.Implementations;

public class TelegramBotUserProvider : ITelegramBotUserProvider
{
    private readonly MonitoringServiceDbContext _monitoringServiceDbContext;
    private readonly ILogger<TelegramBotUserProvider> _logger;

    public TelegramBotUserProvider(MonitoringServiceDbContext monitoringServiceDbContext, ILogger<TelegramBotUserProvider> logger)
    {
        _monitoringServiceDbContext = monitoringServiceDbContext;
        _logger = logger;
    }

    public async Task Save(long chatId, CancellationToken cancellationToken)
    {
        var userExists = await _monitoringServiceDbContext.TelegramBotUsers
            .AsNoTracking()
            .AnyAsync(x => x.TelegramChatId == chatId, cancellationToken);

        if (userExists)
        {
            return;
        }

        var telegramBotUserDbModel = new TelegramBotUserDbModel() { TelegramChatId = chatId };

        await _monitoringServiceDbContext.TelegramBotUsers.AddAsync(telegramBotUserDbModel, cancellationToken);

        await _monitoringServiceDbContext.SaveChangesAsync(cancellationToken);
    }
}