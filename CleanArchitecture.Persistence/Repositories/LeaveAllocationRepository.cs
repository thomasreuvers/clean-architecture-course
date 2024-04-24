using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain;
using CleanArchitecture.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Persistence.Repositories;

public class LeaveAllocationRepository(CaDatabaseContext context) : GenericRepository<LeaveAllocation>(context), ILeaveAllocationRepository
{
    public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
    {
        var leaveAllocation = await Context.LeaveAllocations
            .Include(q => q.LeaveType)
            .FirstOrDefaultAsync(q => q.Id == id);

        return leaveAllocation;
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails()
    {
        var leaveAllocations = await Context.LeaveAllocations
            .Include(q => q.LeaveType)
            .ToListAsync();

        return leaveAllocations;
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(string userId)
    {
        var leaveAllocations = await Context.LeaveAllocations
            .Where(q => q.EmployeeId == userId)
            .Include(q => q.LeaveType)
            .ToListAsync();

        return leaveAllocations;
    }

    public async Task<bool> AllocationExists(int leaveTypeId, string employeeId, int period)
    {
        return await Context.LeaveAllocations
            .AnyAsync(q => q.EmployeeId == employeeId 
                           && q.LeaveTypeId == leaveTypeId 
                           && q.Period == period);
    }

    public async Task AddAllocations(List<LeaveAllocation> allocations)
    {
        await Context.AddRangeAsync(allocations);
        await SaveChangesAsync();
    }

    public async Task<LeaveAllocation> GetEmployeeAllocationsByType(string employeeId, int leaveTypeId)
    {
        return await context.LeaveAllocations.FirstOrDefaultAsync(q => q.EmployeeId == employeeId 
                                                                       && q.LeaveTypeId == leaveTypeId);
    }
}