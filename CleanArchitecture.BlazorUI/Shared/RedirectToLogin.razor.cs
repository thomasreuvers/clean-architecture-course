using Microsoft.AspNetCore.Components;

namespace CleanArchitecture.BlazorUI.Shared;

public partial class RedirectToLogin
{
    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;
    
    protected override void OnInitialized()
    {
        NavigationManager.NavigateTo("login");
    }
}