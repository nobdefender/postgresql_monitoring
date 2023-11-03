using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Monitoring.Posgresql.Infrastructure.Models.Auth;

namespace Monitoring.Posgresql.Infrastructure.Configurations;

public class UserDbModelConfiguration : IEntityTypeConfiguration<UserDbModel>
{
    public void Configure(EntityTypeBuilder<UserDbModel> builder)
    {
        builder.Property(p => p.Id).UseIdentityColumn();
        builder.Property(p => p.Name);
        builder.Property(p => p.Password);
        builder.Property(p => p.Role);
        builder.Property(p => p.Username);
        builder.Property(p => p.EmailAddress);
        builder.Property(p => p.LastName);

        builder.HasKey(x => x.Id).HasName("PK_UserModel");

        builder.HasData(
        new UserDbModel
        {
            Id = 1,
            Name = "User",
            LastName = "User",
            Username = "User",
            EmailAddress = "user@user.ru",
            Password = "FRy0cmdAIeEgGM0h8LZxBZEy7DqwQnrnCPypjg8ia5WJ9+WFt3niUqDHMcYl/0YyTwsD5GV8eqbJQBjy1biTrg==",
            Role = "User",
        },
        new UserDbModel
        {
            Id = 2,
            Name = "Reviewer",
            LastName = "Reviewer",
            Username = "Reviewer",
            EmailAddress = "reviewer@reviewer.ru",
            Password = "lAwsp713uLD9oPruO35jS4zRvH6vXDXi/kJefpm2H7tJNvb/2ugxQh/+90I+olIKH+ifVTd/ZOXH7O4azKp/Wg==",
            Role = "Reviewer",
        });
    }
}