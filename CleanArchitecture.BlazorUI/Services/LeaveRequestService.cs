using AutoMapper;
using Blazored.LocalStorage;
using CleanArchitecture.BlazorUI.Contracts;
using CleanArchitecture.BlazorUI.Models.LeaveAllocations;
using CleanArchitecture.BlazorUI.Models.LeaveRequests;
using CleanArchitecture.BlazorUI.Services.Base;

namespace CleanArchitecture.BlazorUI.Services;

public class LeaveRequestService(
    IClient client,
    ILocalStorageService localStorageService,
    IMapper mapper) 
    : BaseHttpService(client, localStorageService), 
        ILeaveRequestService
{
    public async Task<AdminLeaveRequestViewVM> GetAdminLeaveRequestList()
    {
        var leaveRequests = await Client.LeaveRequestsAllAsync(isLoggedInUser: false);

        var model = new AdminLeaveRequestViewVM
        {
            TotalRequests = leaveRequests.Count,
            ApprovedRequests = leaveRequests.Count(q => q.Approved == true),
            PendingRequests = leaveRequests.Count(q => q.Approved == null),
            RejectedRequests = leaveRequests.Count(q => q.Approved == false),
            LeaveRequests = mapper.Map<List<LeaveRequestVM>>(leaveRequests)
        };

        return model;
    }

    public async Task<EmployeeLeaveRequestViewVM> GetUserLeaveRequests()
    {
        var leaveRequests = await Client.LeaveRequestsAllAsync(isLoggedInUser: true);
        var leaveAllocations = await Client.LeaveAllocationsAllAsync(isLoggedInUser: true);
        var model = new EmployeeLeaveRequestViewVM
        {
            LeaveAllocations = mapper.Map<List<LeaveAllocationVM>>(leaveAllocations),
            LeaveRequests = mapper.Map<List<LeaveRequestVM>>(leaveRequests)
        };

        return model;
    }

    public async Task<LeaveRequestVM> GetLeaveRequest(int id)
    {
        var leaveRequest = await Client.LeaveRequestsGETAsync(id);
        return mapper.Map<LeaveRequestVM>(leaveRequest);
    }

    public async Task<Response<Guid>> CreateLeaveRequest(LeaveRequestVM leaveRequest)
    {
        try
        {
            var response = new Response<Guid>();
            var createLeaveRequest = mapper.Map<CreateLeaveRequestCommand>(leaveRequest);

            await Client.LeaveRequestsPOSTAsync(createLeaveRequest);
            return response;
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    public Task DeleteLeaveRequest(int id)
    {
        throw new NotImplementedException();
    }

    public async Task ApproveLeaveRequest(int id, bool approved)
    {
        try
        {
            var request = new ChangeLeaveRequestApprovalCommand
            {
                Approved = approved,
                Id = id
            };

            await Client.UpdateApprovalAsync(request);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Response<Guid>> CancelLeaveRequest(int id)
    {
        try
        {
            var response = new Response<Guid>();
            var request = new CancelLeaveRequestCommand
            {
                Id = id
            };
            await Client.CancelRequestAsync(request);
            
            return response;
        }
        catch (ApiException e)
        {
            return ConvertApiExceptions<Guid>(e);
        }
    }
}