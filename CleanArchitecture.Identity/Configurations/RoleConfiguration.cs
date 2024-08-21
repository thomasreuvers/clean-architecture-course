using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Identity.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole
            {
                Id = "kwae4a6e-3b1b-4b4f-9b1d-6b0d1f8f1f1b",
                Name = "Employee",
                NormalizedName = "EMPLOYEE"
            },
            new IdentityRole
            {
                Id = "sa1c54ae-3b1b-4b4f-9b1d-6b0d1f8f1f1b",
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            });
    }
}