using MediatR;

namespace CleanArchitecture.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommand : IRequest<Unit>
{
    public int LeaveTypeId { get; set; }   
}