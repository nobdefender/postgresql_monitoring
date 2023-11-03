using Microsoft.EntityFrameworkCore;
using Monitoring.Posgresql.Infrastructure.Configurations;
using Monitoring.Posgresql.Infrastructure.Extensions;
using Monitoring.Posgresql.Infrastructure.Models.Auth;

namespace Monitoring.Posgresql.Infrastructure;

public class MonitoringServiceDbContext : DbContext
{
    public DbSet<UserDbModel> Users { get; set; } = null!;

    public MonitoringServiceDbContext(DbContextOptions<MonitoringServiceDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserDbModelConfiguration());
        modelBuilder.RemovePluralizingTableNameConvention();
    }
}