using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Monitoring.Posgresql.Infrastructure.Models.Access;

namespace Monitoring.Posgresql.Infrastructure.Configurations;

public class ActionDbModelConfiguration : IEntityTypeConfiguration<ActionDbModel>
{
    public void Configure(EntityTypeBuilder<ActionDbModel> builder)
    {
        builder.Property(p => p.Id).UseIdentityColumn();
        builder.Property(p => p.Actionid);
        builder.Property(p => p.Name);
        builder.Property(p => p.Eventsource);
        builder.Property(p => p.Status);
        builder.Property(p => p.Esc_period);
        builder.Property(p => p.Pause_suppressed);
        builder.Property(p => p.Notify_if_canceled);
        builder.Property(p => p.Pause_suppressed);
    }
}