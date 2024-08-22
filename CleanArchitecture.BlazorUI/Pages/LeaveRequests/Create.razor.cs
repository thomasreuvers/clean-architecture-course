using CleanArchitecture.BlazorUI.Contracts;
using CleanArchitecture.BlazorUI.Models.LeaveRequests;
using CleanArchitecture.BlazorUI.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;

namespace CleanArchitecture.BlazorUI.Pages.LeaveRequests;

public partial class Create : ComponentBase
{
    [Inject]
    ILeaveTypeService LeaveTypeService { get; set; }
    
    [Inject]
    ILeaveRequestService LeaveRequestService { get; set; }
    
    [Inject]
    NavigationManager NavigationManager { get; set; }
    
    LeaveRequestVM LeaveRequest { get; set; } = new();
    
    List<LeaveTypeVM> LeaveTypeVMs { get; set; } = [];

    protected override async Task OnInitializedAsync()
    {
        LeaveTypeVMs = await LeaveTypeService.GetLeaveTypes();
    }

    private async Task HandleValidSubmit()
    {
        // Handle the submit logic here
        await LeaveRequestService.CreateLeaveRequest(LeaveRequest);
        NavigationManager.NavigateTo("/leaverequests/");
    }
}