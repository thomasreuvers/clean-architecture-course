using AutoMapper;
using CleanArchitecture.Application.Contracts.Email;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Application.Models.Email;
using MediatR;

namespace CleanArchitecture.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;

public class ChangeLeaveRequestApprovalCommandHandler(
    IMapper mapper,
    IEmailSender emailSender,
    ILeaveRequestRepository leaveRequestRepository,
    ILeaveTypeRepository leaveTypeRepository,
    ILeaveAllocationRepository leaveAllocationRepository) : IRequestHandler<ChangeLeaveRequestApprovalCommand, Unit>
{
    
    public async Task<Unit> Handle(ChangeLeaveRequestApprovalCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = await leaveRequestRepository.GetByIdAsync(request.Id);

        if (leaveRequest is null)
        {
            throw new NotFoundException(nameof(leaveRequest), request.Id);
        }
        
        leaveRequest.Approved = request.Approved;

        await leaveRequestRepository.UpdateAsync(leaveRequest);
        
        // If the request is approved, get and update the employee's allocations.
        if (request.Approved)
        {
            var daysRequested = (int)(leaveRequest.EndDate - leaveRequest.StartDate).TotalDays;
            var allocation = await leaveAllocationRepository.GetUserAllocations(
                leaveRequest.RequestingEmployeeId, leaveRequest.LeaveTypeId);
            allocation.NumberOfDays -= daysRequested;
            
            await leaveAllocationRepository.UpdateAsync(allocation);
        }

        try
        {
            // Send confirmation email
            var email = new EmailMessage
            {
                To = string.Empty,
                Body = $"Your leave request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} has been {(request.Approved ? "approved" : "rejected")}.",
                Subject = "Leave Request Approval"
            };

            await emailSender.SendEmail(email);
        }
        catch (Exception)
        {
            // Log Error
        }
        
        return Unit.Value;
    }
}