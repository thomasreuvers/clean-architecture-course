using CleanArchitecture.BlazorUI.Contracts;
using CleanArchitecture.BlazorUI.Providers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace CleanArchitecture.BlazorUI.Pages;

public partial class Home
{
    [Inject] 
    public NavigationManager NavigationManager { get; set; } = default!;
    
    [Inject]
    private IAuthenticationService AuthenticationService { get; set; } = default!;
    
    [Inject]
    private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

    
    protected override async Task OnInitializedAsync()
    {
        await ((ApiAuthenticationStateProvider)
            AuthenticationStateProvider).GetAuthenticationStateAsync();
    }

    private void GoToLogin()
    {
        NavigationManager.NavigateTo("login/");
    }

    private void GoToRegister()
    {
        NavigationManager.NavigateTo("register/");
    }

    private async void Logout()
    {
        await AuthenticationService.Logout();
    }
}