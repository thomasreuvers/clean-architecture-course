using AutoMapper;
using CleanArchitecture.Application.Contracts.Email;
using CleanArchitecture.Application.Contracts.Logging;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Application.Models.Email;
using MediatR;

namespace CleanArchitecture.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;

public class UpdateLeaveRequestCommandHandler(
    IMapper mapper,
    IEmailSender emailSender,
    ILeaveTypeRepository leaveTypeRepository,
    ILeaveRequestRepository leaveRequestRepository,
    IAppLogger<UpdateLeaveRequestCommandHandler> appLogger) : IRequestHandler<UpdateLeaveRequestCommand, Unit>
{
    public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = await leaveRequestRepository.GetByIdAsync(request.Id);
        
        if (leaveRequest == null)
        {
            throw new NotFoundException(nameof(Domain.LeaveRequest), request.Id);
        }

        var validator = new UpdateLeaveRequestCommandValidator(leaveTypeRepository, leaveRequestRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            throw new BadRequestException("Invalid Leave Request", validationResult);
        }

        mapper.Map(request, leaveRequest);
        
        await leaveRequestRepository.UpdateAsync(leaveRequest);

        try
        {
            var email = new EmailMessage
            {
                To = string.Empty,
                Body =
                    $"Your leave request for {request.StartDate:D} to {request.EndDate:D} has been updated successfully.",
                Subject = "Leave Request Submitted"
            };

            await emailSender.SendEmail(email);
        }
        catch (Exception e)
        {
            appLogger.LogWarning(e.Message);
        }

        return Unit.Value;
    }
}