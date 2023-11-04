using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Monitoring.Posgresql.Infrastructure.Models.WebAuth;

namespace Monitoring.Posgresql.Infrastructure.Configurations;

public class WebUserDbModelConfiguration : IEntityTypeConfiguration<WebUserDbModel>
{
    public void Configure(EntityTypeBuilder<WebUserDbModel> builder)
    {
        builder.Property(p => p.Id).UseIdentityColumn();
        builder.Property(p => p.Name);
        builder.Property(p => p.Password);
        builder.Property(p => p.Role);
        builder.Property(p => p.Username);
        builder.Property(p => p.EmailAddress);
        builder.Property(p => p.LastName);

        builder.HasData(
            new WebUserDbModel
            {
                Id = 1,
                Name = "Admin",
                LastName = "Admin_lastname",
                Username = "Admin_username",
                EmailAddress = "admin@gmail.ru",
                Password = "oKPLwtnO+/yAnIiNhuDVDtOUwo67CERyInTV3MV66r0DJBFFcUdMnoLCoPj0LpClIHHeCCs9169KJisL6o7VfQ==",
                Role = "Admin",
            });
    }
}