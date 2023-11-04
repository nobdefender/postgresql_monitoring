using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Monitoring.Posgresql.Infrastructure;
using Monitoring.Posgresql.Infrastructure.Models.TelegramBot;
using Monitoring.Postgresql.Providers.Interfaces;

namespace Monitoring.Postgresql.Providers.Implementations;

public class TelegramBotUserProvider : ITelegramBotUserProvider
{
    private readonly MonitoringServiceDbContext _monitoringServiceDbContext;
    private readonly ILogger<TelegramBotUserProvider> _logger;
    private readonly IMapper _mapper;

    public TelegramBotUserProvider(MonitoringServiceDbContext monitoringServiceDbContext,
        ILogger<TelegramBotUserProvider> logger, IMapper mapper)
    {
        _monitoringServiceDbContext = monitoringServiceDbContext;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TelegramBotUserDbModel>> GetAllTelegramBotUsersAsync(
        CancellationToken cancellationToken)
    {
        var users = _monitoringServiceDbContext.TelegramBotUsers;
        return await users.ToListAsync(cancellationToken);
    }

    public async Task Save(long chatId, CancellationToken cancellationToken)
    {
        var userExists = await _monitoringServiceDbContext.TelegramBotUsers
            .AsNoTracking()
            .AnyAsync(x => x.TelegramChatId == chatId, cancellationToken);

        if (!userExists)
        {
            var telegramBotUserDbModel = new TelegramBotUserDbModel() { TelegramChatId = chatId };
            await _monitoringServiceDbContext.TelegramBotUsers.AddAsync(telegramBotUserDbModel, cancellationToken);

            await _monitoringServiceDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}