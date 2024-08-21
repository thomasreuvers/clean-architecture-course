using CleanArchitecture.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Identity.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        var hasher = new PasswordHasher<ApplicationUser>();
        builder.HasData(
            new ApplicationUser
            {
                Id = "c1c1b3b4-3b1b-4b4f-9b1d-6b0d1f8f1f1b",
                UserName = "admin@localhost.com",
                NormalizedUserName = "ADMIN@LOCALHOST.COM",
                Email = "admin@localhost.com",
                NormalizedEmail = "ADMIN@LOCALHOST.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Admin@123"),
                FirstName = "Admin",
                LastName = "Admin"
            },
            new ApplicationUser
            {
                Id = "cac54a6e-3b1b-4b4f-9b1d-6b0d1f8f1f1b",
                UserName = "user@localhost.com",
                NormalizedUserName = "USER@LOCALHOST.COM",
                Email = "user@localhost.com",
                NormalizedEmail = "USER@LOCALHOST.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "User@123"),
                FirstName = "User",
                LastName = "User"
            });
    }
}