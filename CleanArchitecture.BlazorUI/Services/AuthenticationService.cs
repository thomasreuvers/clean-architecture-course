using Blazored.LocalStorage;
using CleanArchitecture.BlazorUI.Contracts;
using CleanArchitecture.BlazorUI.Providers;
using CleanArchitecture.BlazorUI.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;

namespace CleanArchitecture.BlazorUI.Services;

public class AuthenticationService(
    IClient client,
    ILocalStorageService localStorageService,
    AuthenticationStateProvider authenticationStateProvider) 
    : BaseHttpService(
        client,
        localStorageService), IAuthenticationService
{
    public async Task<bool> AuthenticateAsync(string email, string password)
    {
        try
        {
            var authenticationRequest = new AuthRequest
            {
                Email = email,
                Password = password
            };

            var authenticationResponse = await Client.LoginAsync(authenticationRequest);

            if (string.IsNullOrEmpty(authenticationResponse.Token)) return false;
            
            await localStorageService.SetItemAsync("token", authenticationResponse.Token);
            
            // Set claims in Blazor and validate login state
            await ((ApiAuthenticationStateProvider)
                    authenticationStateProvider).LoggedIn();
            
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> RegisterAsync(string firstName, string lastName, string userName, string email, string password)
    {
        var registrationRequest = new RegistrationRequest
        {
            FirstName = firstName,
            LastName = lastName,
            UserName = userName,
            Email = email,
            Password = password
        };

        var response = await Client.RegisterAsync(registrationRequest);

        return !string.IsNullOrEmpty(response.UserId);
    }

    public async Task Logout()
    {
        await localStorageService.RemoveItemAsync("token");
        
        // Remove claims in Blazor and invalidate login state
        await ((ApiAuthenticationStateProvider)
            authenticationStateProvider).LoggedOut();
    }
}