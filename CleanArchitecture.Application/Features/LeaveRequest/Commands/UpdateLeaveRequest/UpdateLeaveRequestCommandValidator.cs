using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Features.LeaveRequest.Shared;
using FluentValidation;

namespace CleanArchitecture.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;

public class UpdateLeaveRequestCommandValidator : AbstractValidator<UpdateLeaveRequestCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly ILeaveRequestRepository _leaveRequestRepository;

    public UpdateLeaveRequestCommandValidator(ILeaveTypeRepository leaveTypeRepository, ILeaveRequestRepository leaveRequestRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _leaveRequestRepository = leaveRequestRepository;
        
        Include(new BaseLeaveRequestValidator(_leaveTypeRepository));
        
        RuleFor(p => p.Id)
            .NotNull()
            .MustAsync(LeaveRequestMustExist)
            .WithMessage("{PropertyName} must be present.");
    }
    
    private async Task<bool> LeaveRequestMustExist(int id, CancellationToken cancellationToken)
    {
        var leaveRequest = await _leaveRequestRepository.GetByIdAsync(id);
        return leaveRequest != null;
    }
}