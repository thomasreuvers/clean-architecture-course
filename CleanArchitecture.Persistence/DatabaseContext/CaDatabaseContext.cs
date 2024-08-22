using CleanArchitecture.Application.Contracts.Identity;
using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Persistence.DatabaseContext;

public class CaDatabaseContext(DbContextOptions<CaDatabaseContext> options, IUserService userService) : DbContext(options)
{
    public DbSet<LeaveType> LeaveTypes { get; set; }
    public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
    public DbSet<LeaveRequest> LeaveRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CaDatabaseContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
                     .Where(q => q.State is EntityState.Added or EntityState.Modified))
        {
            entry.Entity.DateModified = DateTime.Now;
            entry.Entity.ModifiedBy = userService.UserId;
            
            if (entry.State == EntityState.Added)
            {
                entry.Entity.DateCreated = DateTime.Now;
                entry.Entity.CreatedBy = userService.UserId;
            }
        }
        
        return base.SaveChangesAsync(cancellationToken);
    }
}