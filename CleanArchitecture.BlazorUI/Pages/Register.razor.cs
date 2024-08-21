using CleanArchitecture.BlazorUI.Contracts;
using CleanArchitecture.BlazorUI.Models;
using Microsoft.AspNetCore.Components;

namespace CleanArchitecture.BlazorUI.Pages;

public partial class Register : ComponentBase
{
    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;
    
    [Inject]
    private IAuthenticationService AuthenticationService { get; set; } = default!;
    
    public RegisterVM Model { get; set; }
    
    public string Message { get; set; }
    
    protected override void OnInitialized()
    {
        Model = new RegisterVM();
    }

    protected async Task HandleRegister()
    {
        var result = await AuthenticationService.RegisterAsync(
            Model.FirstName,
            Model.LastName,
            Model.UserName,
            Model.Email,
            Model.Password);

        if (result)
        {
            NavigationManager.NavigateTo("/");
        }
        
        Message = "Something went wrong, please try again.";
    }
}