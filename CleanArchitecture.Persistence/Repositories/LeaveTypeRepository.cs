using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain;
using CleanArchitecture.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Persistence.Repositories;

public class LeaveTypeRepository(CaDatabaseContext context) 
    : GenericRepository<LeaveType>(context), ILeaveTypeRepository
{
    public Task<bool> IsLeaveTypeUnique(string name)
    {
        return Context.LeaveTypes.AnyAsync(q => q.Name == name);
    }
}