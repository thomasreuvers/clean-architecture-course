using System.Net.Http.Headers;
using Blazored.LocalStorage;

namespace CleanArchitecture.BlazorUI.Services.Base;

public class BaseHttpService(
    IClient client,
    ILocalStorageService localStorageService)
{
    protected IClient Client = client;

    protected Response<Guid> ConvertApiExceptions(ApiException ex)
    {
        return ex.StatusCode switch
        {
            400 => new Response<Guid>
            {
                Message = "Invalid data was submitted", 
                ValidationErrors = ex.Response, 
                IsSuccess = false
            },
            404 => new Response<Guid>
            {
                Message = "The record was not found",
                IsSuccess = false
            },
            _ => new Response<Guid>
            {
                Message = "Something went wrong, please try again later.", 
                IsSuccess = false
            }
        };
    }
    
    protected async Task AddBearerToken()
    {
        if(await localStorageService.ContainKeyAsync("token"))
            client.HttpClient.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Bearer", 
                    await localStorageService.GetItemAsync<string>("token"));
    }
}