namespace CleanArchitecture.BlazorUI.Services.Base;

public partial class Client : IClient
{
    public HttpClient HttpClient => _httpClient;
}