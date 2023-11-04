using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Monitoring.Posgresql.Infrastructure.Models.Bindings;

namespace Monitoring.Posgresql.Infrastructure.Configurations;

public class UserToActionDbModelConfiguration: IEntityTypeConfiguration<UserToActionDbModel>
{
    public void Configure(EntityTypeBuilder<UserToActionDbModel> builder)
    {
        builder.Property(p => p.Id).UseIdentityColumn();
        builder.Property(p => p.UserId);
        builder.Property(p => p.ActiondId);

        builder.HasOne(x => x.UserDbModel).WithMany(x => x.UserToActionDbModels).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.ActionDbModel).WithMany(x => x.UserToActionDbModels).HasForeignKey(x => x.ActiondId).OnDelete(DeleteBehavior.Cascade);
    }
}