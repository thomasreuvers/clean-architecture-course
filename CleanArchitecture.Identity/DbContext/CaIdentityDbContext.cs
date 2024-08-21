using CleanArchitecture.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Identity.DbContext;


public class CaIdentityDbContext(DbContextOptions<CaIdentityDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(CaIdentityDbContext).Assembly);
    }
}