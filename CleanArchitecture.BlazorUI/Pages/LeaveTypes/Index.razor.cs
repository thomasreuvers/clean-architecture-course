using CleanArchitecture.BlazorUI.Contracts;
using CleanArchitecture.BlazorUI.Models.LeaveTypes;
using CleanArchitecture.BlazorUI.Providers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace CleanArchitecture.BlazorUI.Pages.LeaveTypes;

public partial class Index
{
    [Inject]
    public NavigationManager NavigationManager { get; set; }
    
    [Inject]
    public ILeaveTypeService LeaveTypeService { get; set; }
    
    [Inject]
    public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
    
    public List<LeaveTypeVM> LeaveTypes { get; private set; }

    public string Message { get; set; } = string.Empty;

    protected void CreateLeaveType()
    {
        NavigationManager.NavigateTo("/leavetypes/create/");
    }

    protected void AllocateLeaveType(int id)
    {
        // Use Leave Allocation Service here
    }

    protected void EditLeaveType(int id)
    {
        NavigationManager.NavigateTo($"/leavetypes/edit/{id}");
    }

    protected void DetailsLeaveType(int id)
    {
        NavigationManager.NavigateTo($"/leavetypes/details/{id}");
    }

    protected async Task DeleteLeaveType(int id)
    {
        var response = await LeaveTypeService.DeleteLeaveType(id);
        if (response.IsSuccess)
        {
            StateHasChanged();
        }
        else
        {
            Message = response.Message;
        }
    }

    protected override async Task OnInitializedAsync()
    {
        await ((ApiAuthenticationStateProvider)AuthenticationStateProvider).GetAuthenticationStateAsync();
        LeaveTypes = await LeaveTypeService.GetLeaveTypes();
    }
}