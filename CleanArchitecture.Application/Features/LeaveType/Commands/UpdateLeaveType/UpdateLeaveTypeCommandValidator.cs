using CleanArchitecture.Application.Contracts.Persistence;
using FluentValidation;

namespace CleanArchitecture.Application.Features.LeaveType.Commands.UpdateLeaveType;

public class UpdateLeaveTypeCommandValidator: AbstractValidator<UpdateLeaveTypeCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    public UpdateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        RuleFor(p => p.Id)
            .NotNull()
            .MustAsync(LeaveTypeMustExist);
        
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
        
        RuleFor(p => p.DefaultDays)
            .GreaterThan(100).WithMessage("{PropertyName} cannot exceed 100.")
            .LessThan(1).WithMessage("{PropertyName} cannot be less than 1");

        RuleFor(q => q)
            .MustAsync(LeaveTypeNameUnique)
            .WithMessage("Leave type already exists");

        _leaveTypeRepository = leaveTypeRepository;
    }
    
    private async Task<bool> LeaveTypeMustExist(int id, CancellationToken token)
    {
        var leaveType = await _leaveTypeRepository.GetByIdAsync(id);
        return leaveType != null;
    }
    
    private async Task<bool> LeaveTypeNameUnique(UpdateLeaveTypeCommand updateLeaveTypeCommand, CancellationToken token)
    {
        return await _leaveTypeRepository.IsLeaveTypeUnique(updateLeaveTypeCommand.Name);
    }
}