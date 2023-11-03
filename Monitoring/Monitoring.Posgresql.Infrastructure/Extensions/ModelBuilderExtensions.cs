using Microsoft.EntityFrameworkCore;

namespace Monitoring.Posgresql.Infrastructure.Extensions;

public static class ModelBuilderExtensions
{
    public static void RemovePluralizingTableNameConvention(this ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            entity.SetTableName(entity.GetTableName());
        }
    }
}