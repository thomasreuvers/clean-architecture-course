using CleanArchitecture.BlazorUI.Contracts;
using CleanArchitecture.BlazorUI.Models.LeaveRequests;
using Microsoft.AspNetCore.Components;

namespace CleanArchitecture.BlazorUI.Pages.LeaveRequests;

public partial class Index : ComponentBase
{
    [Inject]
    ILeaveRequestService leaveRequestService { get; set; }
    
    [Inject]
    NavigationManager NavigationManager { get; set; }
    
    public AdminLeaveRequestViewVM Model { get; set; } = new();
    
    protected override async Task OnInitializedAsync()
    {
        Model = await leaveRequestService.GetAdminLeaveRequestList();
    }
    
    void GoToDetails(int id)
    {
        NavigationManager.NavigateTo($"/leaverequests/details/{id}");
    }
}