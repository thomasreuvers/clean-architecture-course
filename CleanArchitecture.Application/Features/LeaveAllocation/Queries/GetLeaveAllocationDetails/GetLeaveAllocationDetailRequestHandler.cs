using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using MediatR;

namespace CleanArchitecture.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;

public class GetLeaveAllocationDetailRequestHandler(
    ILeaveAllocationRepository leaveAllocationRepository,
    IMapper mapper)
    : IRequestHandler<GetLeaveAllocationDetailQuery, LeaveAllocationDetailsDto>
{
    public async Task<LeaveAllocationDetailsDto> Handle(GetLeaveAllocationDetailQuery request,
        CancellationToken cancellationToken)
    {
        var leaveAllocation = await leaveAllocationRepository.GetLeaveAllocationWithDetails(request.Id);
        return mapper.Map<LeaveAllocationDetailsDto>(leaveAllocation);
    }
}