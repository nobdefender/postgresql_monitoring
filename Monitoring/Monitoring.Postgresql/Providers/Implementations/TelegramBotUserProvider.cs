using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Monitoring.Posgresql.Infrastructure;
using Monitoring.Posgresql.Infrastructure.Models.TelegramBot;
using Monitoring.Postgresql.Models.Auth;
using Monitoring.Postgresql.Providers.Interfaces;

namespace Monitoring.Postgresql.Providers.Implementations;

public class TelegramBotUserProvider : ITelegramBotUserProvider
{
    private readonly IMapper _mapper;
    private readonly MonitoringServiceDbContext _monitoringServiceDbContext;

    public TelegramBotUserProvider(MonitoringServiceDbContext monitoringServiceDbContext, IMapper mapper)
    {
        _mapper = mapper;
        _monitoringServiceDbContext = monitoringServiceDbContext;
    }

    public async Task<IEnumerable<TelegramBotUserDbModel>> GetAllTelegramBotUsersAsync(
        CancellationToken cancellationToken)
    {
        var users = _monitoringServiceDbContext.TelegramBotUsers;
        return await users.ToListAsync(cancellationToken);
    }
}