using CleanArchitecture.BlazorUI.Services.Base;

namespace CleanArchitecture.BlazorUI.Contracts;

public interface ILeaveAllocationService
{
    Task<Response<Guid>> CreateLeaveAllocation(int leaveTypeId);
}