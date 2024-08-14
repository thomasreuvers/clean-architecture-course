using AutoMapper;
using CleanArchitecture.BlazorUI.Contracts;
using CleanArchitecture.BlazorUI.Models.LeaveTypes;
using CleanArchitecture.BlazorUI.Services.Base;

namespace CleanArchitecture.BlazorUI.Services;

public class LeaveTypeService(IClient client, IMapper mapper) : BaseHttpService(client), ILeaveTypeService
{
    public async Task<List<LeaveTypeVM>> GetLeaveTypes()
    {
        var leaveTypes = await Client.LeaveTypesAllAsync();
        return mapper.Map<List<LeaveTypeVM>>(leaveTypes);
    }

    public async Task<LeaveTypeVM> GetLeaveTypeDetails(int id)
    {
        var leaveType = await Client.LeaveTypesGETAsync(id);
        return mapper.Map<LeaveTypeVM>(leaveType);
    }

    public async Task<Response<Guid>> CreateLeaveType(LeaveTypeVM leaveType)
    {
        try
        {
            var createLeaveTypeCommand = mapper.Map<CreateLeaveTypeCommand>(leaveType);
            await Client.LeaveTypesPOSTAsync(createLeaveTypeCommand);
            return new Response<Guid>
            {
                IsSuccess = true
            };
        }
        catch (ApiException e)
        {
            return ConvertApiExceptions(e);
        }
    }

    public async Task<Response<Guid>> UpdateLeaveType(int id, LeaveTypeVM leaveType)
    {
        try
        {
            var updateLeaveTypeCommand = mapper.Map<UpdateLeaveTypeCommand>(leaveType);
            await Client.LeaveTypesPUTAsync(id.ToString(), updateLeaveTypeCommand);
            return new Response<Guid>
            {
                IsSuccess = true
            };
        }
        catch (ApiException e)
        {
            return ConvertApiExceptions(e);
        }
    }

    public async Task<Response<Guid>> DeleteLeaveType(int id)
    {
        try
        {
            await Client.LeaveTypesDELETEAsync(id);
            return new Response<Guid>
            {
                IsSuccess = true
            };
        }
        catch (ApiException e)
        {
            return ConvertApiExceptions(e);
        }
    }
}