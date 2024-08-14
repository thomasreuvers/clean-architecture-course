using CleanArchitecture.BlazorUI.Models.LeaveTypes;
using CleanArchitecture.BlazorUI.Services.Base;

namespace CleanArchitecture.BlazorUI.Contracts;

public interface ILeaveTypeService
{
    Task<List<LeaveTypeVM>> GetLeaveTypes();
    Task<LeaveTypeVM> GetLeaveTypeDetails(int id);
    
    Task<Response<Guid>> CreateLeaveType(LeaveTypeVM leaveType);
    
    Task<Response<Guid>> UpdateLeaveType(int id, LeaveTypeVM leaveType);
    
    Task<Response<Guid>> DeleteLeaveType(int id);
}