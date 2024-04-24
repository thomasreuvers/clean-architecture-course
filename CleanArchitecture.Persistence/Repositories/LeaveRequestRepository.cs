using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain;
using CleanArchitecture.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Persistence.Repositories;

public class LeaveRequestRepository(CaDatabaseContext context) : GenericRepository<LeaveRequest>(context), ILeaveRequestRepository
{
    public async Task<LeaveRequest> GetLeaveRequestWithDetails(int id)
    {
        var leaveRequest = await Context.LeaveRequests
            .Where(q => q.Id == id)
            .Include(q => q.LeaveType)
            .FirstAsync();
        return leaveRequest;
    }

    public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails()
    {
        var leaveRequests = await Context.LeaveRequests
            .Include(q => q.LeaveType)
            .ToListAsync();
        return leaveRequests;
    }

    public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails(string userId)
    {
        var leaveRequests = await Context.LeaveRequests
            .Where(q => q.RequestingEmployeeId == userId)
            .Include(q => q.LeaveType)
            .ToListAsync();

        return leaveRequests;
    }
}