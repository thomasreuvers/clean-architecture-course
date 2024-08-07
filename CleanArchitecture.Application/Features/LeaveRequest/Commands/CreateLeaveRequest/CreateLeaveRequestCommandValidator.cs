using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Features.LeaveRequest.Shared;
using FluentValidation;

namespace CleanArchitecture.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;

public class CreateLeaveRequestCommandValidator : AbstractValidator<CreateLeaveRequestCommand>
{
    public CreateLeaveRequestCommandValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        Include(new BaseLeaveRequestValidator(leaveTypeRepository));
    }
}