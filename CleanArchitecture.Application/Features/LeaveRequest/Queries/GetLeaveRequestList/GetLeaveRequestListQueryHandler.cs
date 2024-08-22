using AutoMapper;
using CleanArchitecture.Application.Contracts.Identity;
using CleanArchitecture.Application.Contracts.Persistence;
using MediatR;

namespace CleanArchitecture.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;

public class GetLeaveRequestListQueryHandler(
    ILeaveRequestRepository leaveRequestRepository,
    IMapper mapper,
    IUserService userService) : IRequestHandler<GetLeaveRequestListQuery, List<LeaveRequestListDto>>
{
    public async Task<List<LeaveRequestListDto>> Handle(GetLeaveRequestListQuery request, CancellationToken cancellationToken)
    {
        var leaveRequests = new List<Domain.LeaveRequest>();
        var requests = mapper.Map<List<LeaveRequestListDto>>(leaveRequests);

        if (request.IsLoggedInUser)
        {
            var userId = userService.UserId;
            leaveRequests = await leaveRequestRepository.GetLeaveRequestsWithDetails();

            var employee = await userService.GetEmployee(userId);
            requests = mapper.Map<List<LeaveRequestListDto>>(leaveRequests);
            foreach (var req in requests)
            {
                req.Employee = employee;
            }
        }
        else
        {
            leaveRequests = await leaveRequestRepository.GetLeaveRequestsWithDetails();
            requests = mapper.Map<List<LeaveRequestListDto>>(leaveRequests);
            foreach (var req in requests)
            {
                req.Employee = await userService.GetEmployee(req.RequestingEmployeeId);
            }
        }

        return requests;
    }
}