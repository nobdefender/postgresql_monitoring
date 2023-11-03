using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Monitoring.Posgresql.Infrastructure.Models.Access;
using Monitoring.Posgresql.Infrastructure.Models.Bindings;

namespace Monitoring.Posgresql.Infrastructure.Configurations;

public class UserToActionDbModelConfiguration: IEntityTypeConfiguration<UserToActionDbModel>
{
    public void Configure(EntityTypeBuilder<UserToActionDbModel> builder)
    {
        builder.Property(p => p.Id).UseIdentityColumn();
        builder.Property(p => p.UserId);
        builder.Property(p => p.ActiondId);
    }
}