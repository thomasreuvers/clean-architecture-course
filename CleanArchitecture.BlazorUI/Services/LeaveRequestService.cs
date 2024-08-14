using CleanArchitecture.BlazorUI.Contracts;
using CleanArchitecture.BlazorUI.Services.Base;

namespace CleanArchitecture.BlazorUI.Services;

public class LeaveRequestService(IClient client) : BaseHttpService(client), ILeaveRequestService
{
    
}