using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Monitoring.Posgresql.Infrastructure.Models.Bindings;

namespace Monitoring.Posgresql.Infrastructure.Configurations;

public class UserToActionDbModelConfiguration : IEntityTypeConfiguration<TelegramToUserToActionDbModel>
{
    public void Configure(EntityTypeBuilder<TelegramToUserToActionDbModel> builder)
    {
        builder.Property(p => p.Id).UseIdentityColumn();
        builder.Property(p => p.TelegramBotUserId);
        builder.Property(p => p.ActiondId);

        builder.HasOne(x => x.TelegramBotUserDbModel).WithMany(x => x.UserToActionDbModels)
            .HasForeignKey(x => x.TelegramBotUserId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.ActionDbModel).WithMany(x => x.UserToActionDbModels).HasForeignKey(x => x.ActiondId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}