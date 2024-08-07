using CleanArchitecture.Application.Contracts.Persistence;
using FluentValidation;

namespace CleanArchitecture.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;

public class UpdateLeaveAllocationCommandValidator
    : AbstractValidator<UpdateLeaveAllocationCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    
    public UpdateLeaveAllocationCommandValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;

        RuleFor(p => p.NumberOfDays)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than {ComparisonValue}");

        RuleFor(p => p.Period)
            .GreaterThanOrEqualTo(DateTime.Now.Year).WithMessage("{PropertyName} must be after {ComparisonValue}");
        
        RuleFor(p => p.LeaveTypeId)
            .GreaterThan(0)
            .MustAsync(LeaveTypeMustExist)
            .WithMessage("{PropertyName} does not exist.");

        RuleFor(p => p.Id).NotNull().WithMessage("{PropertyName} must be present");
    }

    private async Task<bool> LeaveTypeMustExist(int id, CancellationToken token)
    {
        var leaveType = await _leaveTypeRepository.GetByIdAsync(id);
        return leaveType != null;
    }
}