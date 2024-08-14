using CleanArchitecture.BlazorUI.Contracts;
using CleanArchitecture.BlazorUI.Services.Base;

namespace CleanArchitecture.BlazorUI.Services;

public class LeaveAllocationService(IClient client) : BaseHttpService(client), ILeaveAllocationService
{
    
}