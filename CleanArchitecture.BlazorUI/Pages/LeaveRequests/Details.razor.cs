using CleanArchitecture.BlazorUI.Contracts;
using CleanArchitecture.BlazorUI.Models.LeaveRequests;
using Microsoft.AspNetCore.Components;

namespace CleanArchitecture.BlazorUI.Pages.LeaveRequests;

public partial class Details : ComponentBase
{
    [Inject]
    ILeaveRequestService leaveRequestService { get; set; }
    
    [Inject]
    NavigationManager NavigationManager { get; set; }
    
    [Parameter]
    public int Id { get; set; }

    private string ClassName;
    private string HeadingText;

    public LeaveRequestVM Model { get; private set; } = new();

    protected override async Task OnParametersSetAsync()
    {
        Model = await leaveRequestService.GetLeaveRequest(Id);
    }

    protected override async Task OnInitializedAsync()
    {
        switch(Model.Approved)
        {
            case true:
                SetApproved();
                break;
            case false:
                SetRejected();
                break;
            default:
                SetPending();
                break;
        }
    }

    async Task ChangeApproval(bool approvalStatus)
    {
        await leaveRequestService.ApproveLeaveRequest(Id, approvalStatus);
        NavigationManager.NavigateTo("/leaverequests/");
    }
    
    private void SetApproved()
    {
        ClassName = "success";
        HeadingText = "Approved";
    }
    
    private void SetRejected()
    {
        ClassName = "danger";
        HeadingText = "Rejected";
    }

    private void SetPending()
    {
        ClassName = "warning";
        HeadingText = "Pending Approval";
    }
}