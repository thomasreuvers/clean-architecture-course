using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using MediatR;

namespace CleanArchitecture.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;

public class GetLeaveRequestListQueryHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper) : IRequestHandler<GetLeaveRequestListQuery, List<LeaveRequestListDto>>
{
    public async Task<List<LeaveRequestListDto>> Handle(GetLeaveRequestListQuery request, CancellationToken cancellationToken)
    {
        var leaveRequests = await leaveRequestRepository.GetLeaveRequestsWithDetails();
        var requests = mapper.Map<List<LeaveRequestListDto>>(leaveRequests);

        return requests;
    }
}