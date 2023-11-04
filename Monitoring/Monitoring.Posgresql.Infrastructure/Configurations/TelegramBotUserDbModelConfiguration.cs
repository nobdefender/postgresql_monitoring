using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Monitoring.Posgresql.Infrastructure.Models.TelegramBot;

namespace Monitoring.Posgresql.Infrastructure.Configurations;

public class TelegramBotUserDbModelConfiguration : IEntityTypeConfiguration<TelegramBotUserDbModel>
{
    public void Configure(EntityTypeBuilder<TelegramBotUserDbModel> builder)
    {
        builder.Property(p => p.Id).UseIdentityColumn();
        builder.Property(p => p.TelegramChatId);
    }
}