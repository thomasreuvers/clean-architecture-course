using CleanArchitecture.BlazorUI.Models.LeaveAllocations;

namespace CleanArchitecture.BlazorUI.Models.LeaveRequests;

public class EmployeeLeaveRequestViewVM
{
    public List<LeaveAllocationVM> LeaveAllocations { get; set; } = new();
    public List<LeaveRequestVM> LeaveRequests { get; set; } = new();
}