using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Exceptions;
using MediatR;

namespace CleanArchitecture.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommandHandler(
    IMapper mapper,
    ILeaveAllocationRepository leaveAllocationRepository,
    ILeaveTypeRepository leaveTypeRepository)
    : IRequestHandler<CreateLeaveAllocationCommand, Unit>
{
    public async Task<Unit> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveAllocationCommandValidator(leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Any())
        {
            throw new BadRequestException("Invalid Leave Allocation Request", validationResult);
        }

        var leaveType = await leaveTypeRepository.GetByIdAsync(request.LeaveTypeId);

        var leaveAllocation = mapper.Map<Domain.LeaveAllocation>(request);
        await leaveAllocationRepository.CreateAsync(leaveAllocation);
        return Unit.Value;
    }
}