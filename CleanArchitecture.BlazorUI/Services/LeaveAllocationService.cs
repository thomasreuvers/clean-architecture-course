using Blazored.LocalStorage;
using CleanArchitecture.BlazorUI.Contracts;
using CleanArchitecture.BlazorUI.Services.Base;

namespace CleanArchitecture.BlazorUI.Services;

public class LeaveAllocationService(
    IClient client,
    ILocalStorageService localStorageService) 
    : BaseHttpService(client, localStorageService), 
        ILeaveAllocationService
{
}