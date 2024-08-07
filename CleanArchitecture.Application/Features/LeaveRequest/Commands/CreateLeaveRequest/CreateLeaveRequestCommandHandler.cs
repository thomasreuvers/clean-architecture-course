using AutoMapper;
using CleanArchitecture.Application.Contracts.Email;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Application.Models.Email;
using MediatR;

namespace CleanArchitecture.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;

public class CreateLeaveRequestCommandHandler(
    IEmailSender emailSender,
    IMapper mapper,
    ILeaveTypeRepository leaveTypeRepository,
    ILeaveRequestRepository leaveRequestRepository)
    : IRequestHandler<CreateLeaveRequestCommand, Unit>
{
    public async Task<Unit> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveRequestCommandValidator(leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            throw new BadRequestException("Invalid Leave Request", validationResult);
        }
        
        // Get requesting employee's id
        
        // check on employee's allocation
        
        // if allocations aren't enough, return validation error with message
        
        // Create leave request
        var leaveRequest = mapper.Map<Domain.LeaveRequest>(request);
        await leaveRequestRepository.CreateAsync(leaveRequest);
        
        // Send confirmation email
        var email = new EmailMessage
        {
            To = string.Empty, /**/
            Body =
                $"Your leave request for {request.StartDate:D} to {request.EndDate:D} has been submitted successfully.",
            Subject = "Leave Request Submitted"
        };
        
        await emailSender.SendEmail(email);
        
        return Unit.Value;
    }
}