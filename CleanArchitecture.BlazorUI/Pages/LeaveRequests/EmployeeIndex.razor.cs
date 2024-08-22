using CleanArchitecture.BlazorUI.Contracts;
using CleanArchitecture.BlazorUI.Models.LeaveRequests;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CleanArchitecture.BlazorUI.Pages.LeaveRequests;

public partial class EmployeeIndex : ComponentBase
{
    [Inject]
    public ILeaveRequestService LeaveRequestService { get; set; }
    
    [Inject]
    IJSRuntime JSRuntime { get; set; }
    
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    public EmployeeLeaveRequestViewVM Model { get; set; } = new();

    public string Message { get; set; } = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        Model = await LeaveRequestService.GetUserLeaveRequests();
    }

    async Task CancelRequestAsync(int id)
    {
        var confirm = await JSRuntime.InvokeAsync<bool>("confirm", "Do you want to cancel this request?");
        if (confirm)
        {
            var response = await LeaveRequestService.CancelLeaveRequest(id);
            if(response.IsSuccess)
            {
                StateHasChanged();
            }
            else
            {
                Message = response.Message;
            }
        }
    }
}