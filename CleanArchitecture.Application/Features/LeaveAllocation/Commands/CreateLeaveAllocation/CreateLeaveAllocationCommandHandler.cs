using AutoMapper;
using CleanArchitecture.Application.Contracts.Identity;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Exceptions;
using MediatR;

namespace CleanArchitecture.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommandHandler(
    ILeaveAllocationRepository leaveAllocationRepository,
    ILeaveTypeRepository leaveTypeRepository,
    IUserService userService)
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
        
        var employees = await userService.GetEmployees();

        var period = DateTime.Now.Year;

        var allocations = new List<Domain.LeaveAllocation>();
        foreach (var emp in employees)
        {
            var allocationExists =
                await leaveAllocationRepository.AllocationExists(request.LeaveTypeId, emp.Id, period);

            if (allocationExists == false)
            {
                allocations.Add(new Domain.LeaveAllocation
                {
                    EmployeeId = emp.Id,
                    LeaveTypeId = leaveType.Id,
                    NumberOfDays = leaveType.DefaultDays,
                    Period = period
                });
            }
        }

        if (allocations.Any())
        {
            await leaveAllocationRepository.AddAllocations(allocations);
        }

        return Unit.Value;
    }
}