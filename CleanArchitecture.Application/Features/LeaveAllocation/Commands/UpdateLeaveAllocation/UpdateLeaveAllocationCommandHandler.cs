using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Exceptions;
using MediatR;

namespace CleanArchitecture.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;

public class UpdateLeaveAllocationCommandHandler(
    IMapper mapper,
    ILeaveAllocationRepository leaveAllocationRepository,
    ILeaveTypeRepository leaveTypeRepository)
    : IRequestHandler<UpdateLeaveAllocationCommand, Unit>
{
    public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateLeaveAllocationCommandValidator(leaveTypeRepository);
        var validatorResult = await validator.ValidateAsync(request, cancellationToken);

        if (validatorResult.Errors.Any())
        {
            throw new BadRequestException("Invalid Leave Allocation", validatorResult);
        }

        var leaveAllocation = await leaveAllocationRepository.GetByIdAsync(request.Id);

        if (leaveAllocation is null)
        {
            throw new NotFoundException(nameof(leaveAllocation), request.Id);
        }

        mapper.Map(request, leaveAllocation);

        await leaveAllocationRepository.UpdateAsync(leaveAllocation);
        return Unit.Value;
    }
}