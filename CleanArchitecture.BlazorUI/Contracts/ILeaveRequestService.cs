using CleanArchitecture.BlazorUI.Models.LeaveRequests;
using CleanArchitecture.BlazorUI.Services.Base;

namespace CleanArchitecture.BlazorUI.Contracts;

public interface ILeaveRequestService
{
    Task<AdminLeaveRequestViewVM> GetAdminLeaveRequestList();
    Task<EmployeeLeaveRequestViewVM> GetUserLeaveRequests();
    Task<LeaveRequestVM> GetLeaveRequest(int id);
    Task<Response<Guid>> CreateLeaveRequest(LeaveRequestVM leaveRequest);
    Task DeleteLeaveRequest(int id);
    Task ApproveLeaveRequest(int id, bool approved);
    Task<Response<Guid>> CancelLeaveRequest(int id);
}