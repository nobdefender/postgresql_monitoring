using Microsoft.EntityFrameworkCore;
using Monitoring.Posgresql.Infrastructure.Configurations;
using Monitoring.Posgresql.Infrastructure.Extensions;
using Monitoring.Posgresql.Infrastructure.Models.Access;
using Monitoring.Posgresql.Infrastructure.Models.Auth;
using Monitoring.Posgresql.Infrastructure.Models.Bindings;

namespace Monitoring.Posgresql.Infrastructure;

public class MonitoringServiceDbContext : DbContext
{
    public DbSet<UserDbModel> Users { get; set; } = null!;
    public DbSet<ActionDbModel> Actions { get; set; } = null!;
    public DbSet<UserToActionDbModel> UserToAction { get; set; } = null!;

    public MonitoringServiceDbContext(DbContextOptions<MonitoringServiceDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserDbModelConfiguration());
        modelBuilder.ApplyConfiguration(new ActionDbModelConfiguration());
        modelBuilder.ApplyConfiguration(new UserToActionDbModelConfiguration());
        modelBuilder.RemovePluralizingTableNameConvention();
    }
}