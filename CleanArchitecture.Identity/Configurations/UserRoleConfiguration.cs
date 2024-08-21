using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Identity.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(
            new IdentityUserRole<string>
            {
                RoleId = "sa1c54ae-3b1b-4b4f-9b1d-6b0d1f8f1f1b",
                UserId = "c1c1b3b4-3b1b-4b4f-9b1d-6b0d1f8f1f1b"
            },
            new IdentityUserRole<string>
            {
                RoleId = "kwae4a6e-3b1b-4b4f-9b1d-6b0d1f8f1f1b",
                UserId = "cac54a6e-3b1b-4b4f-9b1d-6b0d1f8f1f1b"
            });
    }
}