using Microsoft.EntityFrameworkCore;
using Monitoring.Posgresql.Infrastructure.Configurations;
using Monitoring.Posgresql.Infrastructure.Extensions;
using Monitoring.Posgresql.Infrastructure.Models.Access;
using Monitoring.Posgresql.Infrastructure.Models.Bindings;
using Monitoring.Posgresql.Infrastructure.Models.TelegramBot;
using Monitoring.Posgresql.Infrastructure.Models.WebAuth;

namespace Monitoring.Posgresql.Infrastructure;

public class MonitoringServiceDbContext : DbContext
{
    public DbSet<TelegramBotUserDbModel> TelegramBotUsers { get; set; } = null!;
    public DbSet<ActionDbModel> Actions { get; set; } = null!;
    public DbSet<TelegramToUserToActionDbModel> TelegramBotUserToAction { get; set; } = null!;
    public DbSet<WebUserDbModel> WebUsers { get; set; } = null!;
    

    public MonitoringServiceDbContext(DbContextOptions<MonitoringServiceDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ActionDbModelConfiguration());
        modelBuilder.ApplyConfiguration(new WebUserDbModelConfiguration());
        modelBuilder.ApplyConfiguration(new UserToActionDbModelConfiguration());
        modelBuilder.ApplyConfiguration(new TelegramBotUserDbModelConfiguration());
        modelBuilder.RemovePluralizingTableNameConvention();
    }
}