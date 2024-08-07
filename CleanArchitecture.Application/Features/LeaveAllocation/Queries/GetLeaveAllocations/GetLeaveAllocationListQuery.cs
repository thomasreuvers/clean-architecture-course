using MediatR;

namespace CleanArchitecture.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;

public class GetLeaveAllocationListQuery : IRequest<List<LeaveAllocationDto>>
{
}