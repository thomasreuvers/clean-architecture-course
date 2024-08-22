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
    public async Task<Response<Guid>> CreateLeaveAllocation(int leaveTypeId)
    {
        try
        {
            var response = new Response<Guid>();
            var createLeaveAllocation = new CreateLeaveAllocationCommand { LeaveTypeId = leaveTypeId };

            await client.LeaveAllocationsPOSTAsync(createLeaveAllocation);
            return response;
        }
        catch (ApiException e)
        {
            return ConvertApiExceptions<Guid>(e);
        }
    }
}