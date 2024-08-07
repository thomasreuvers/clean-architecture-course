using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using MediatR;

namespace CleanArchitecture.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail;

public class GetLeaveRequestDetailQueryHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper) : IRequestHandler<GetLeaveRequestDetailQuery, LeaveRequestDetailsDto>
{
    public async Task<LeaveRequestDetailsDto> Handle(GetLeaveRequestDetailQuery request, CancellationToken cancellationToken)
    {
        var leaveRequest =
            mapper.Map<LeaveRequestDetailsDto>(await leaveRequestRepository.GetLeaveRequestWithDetails(request.Id));

        return leaveRequest;
    }
}