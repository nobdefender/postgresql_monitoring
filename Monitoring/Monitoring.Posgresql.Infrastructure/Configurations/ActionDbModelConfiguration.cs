using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Monitoring.Posgresql.Infrastructure.Models.Access;

namespace Monitoring.Posgresql.Infrastructure.Configurations;

public class ActionDbModelConfiguration : IEntityTypeConfiguration<ActionDbModel>
{
    public void Configure(EntityTypeBuilder<ActionDbModel> builder)
    {
        builder.Property(p => p.Id).UseIdentityColumn();
        builder.Property(p => p.Name);
    }
}