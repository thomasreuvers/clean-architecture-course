using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using MediatR;

namespace CleanArchitecture.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;

public class GetLeaveAllocationListHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper)
    : IRequestHandler<GetLeaveAllocationListQuery, List<LeaveAllocationDto>>
{
    public async Task<List<LeaveAllocationDto>> Handle(GetLeaveAllocationListQuery request, CancellationToken cancellationToken)
    {
        var leaveAllocations = await leaveAllocationRepository.GetLeaveAllocationsWithDetails();
        var mappedLeaveAllocations = mapper.Map<List<LeaveAllocationDto>>(leaveAllocations);
        
        return mappedLeaveAllocations;
    }
}