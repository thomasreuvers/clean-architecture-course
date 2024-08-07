using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Exceptions;
using MediatR;

namespace CleanArchitecture.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation;

public class DeleteLeaveAllocationCommandHandler(
    IMapper mapper,
    ILeaveAllocationRepository leaveAllocationRepository,
    ILeaveTypeRepository leaveTypeRepository)
    : IRequestHandler<DeleteLeaveAllocationCommand, Unit>
{
    public async Task<Unit> Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var leaveAllocation = await leaveAllocationRepository.GetByIdAsync(request.Id);

        if (leaveAllocation is null)
        {
            throw new NotFoundException(nameof(leaveAllocation), request.Id);
        }

        await leaveAllocationRepository.DeleteAsync(leaveAllocation);
        return Unit.Value;
    }
}