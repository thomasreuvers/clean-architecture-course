using CleanArchitecture.Domain;

namespace CleanArchitecture.Application.Contracts.Persistence;

public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
{
    Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id);
    Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails();
    Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(string userId);
    Task<bool> AllocationExists(int leaveTypeId, string employeeId, int period);
    Task AddAllocations(List<LeaveAllocation> allocations);
    Task<LeaveAllocation> GetUserAllocations(string employeeId, int leaveTypeId);
}