using CleanArchitecture.BlazorUI.Contracts;
using CleanArchitecture.BlazorUI.Models;
using Microsoft.AspNetCore.Components;

namespace CleanArchitecture.BlazorUI.Pages;

public partial class Login : ComponentBase
{
    [Inject] 
    public NavigationManager NavigationManager { get; set; } = default!;
    
    [Inject]
    private IAuthenticationService AuthenticationService { get; set; } = default!;
    
    public LoginVM Model { get;set; }
    public string Message { get; set; }

    protected override void OnInitialized()
    {
        Model = new LoginVM();
    }

    protected async Task HandleLogin()
    {
        if (await AuthenticationService.AuthenticateAsync(Model.Email, Model.Password))
        {
            NavigationManager.NavigateTo("/");
        }

        Message = "Username/Password is incorrect. Please try again.";
    }
}