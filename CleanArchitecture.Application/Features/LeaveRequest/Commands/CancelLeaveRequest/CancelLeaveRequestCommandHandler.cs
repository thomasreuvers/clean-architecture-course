using CleanArchitecture.Application.Contracts.Email;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Application.Models.Email;
using MediatR;

namespace CleanArchitecture.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;

public class CancelLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, IEmailSender emailSender, ILeaveAllocationRepository leaveAllocationRepository)
    : IRequestHandler<CancelLeaveRequestCommand, Unit>
{
    public async Task<Unit> Handle(CancelLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = await leaveRequestRepository.GetByIdAsync(request.Id);

        if (leaveRequest is null)
        {
            throw new NotFoundException(nameof(leaveRequest), request.Id);
        }
        
        leaveRequest.Cancelled = true;
        await leaveRequestRepository.UpdateAsync(leaveRequest);
        
        // If already approved, re-evaluate the employee's allocations for the leave type
        if (leaveRequest.Approved == true)
        {
            var daysRequested = (int)(leaveRequest.EndDate - leaveRequest.StartDate).TotalDays;
            var allocation =
                await leaveAllocationRepository.GetUserAllocations(leaveRequest.RequestingEmployeeId, leaveRequest.LeaveTypeId);
            allocation.NumberOfDays += daysRequested;
        
            await leaveAllocationRepository.UpdateAsync(allocation);
        }
        
        try
        {
            // Send conformation email
            var email = new EmailMessage
            {
                To = string.Empty,
                Body = $"Your leave request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} has been cancelled.",
                Subject = "Leave Request Cancelled"
            };
        
            await emailSender.SendEmail(email);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        return Unit.Value;
    }
}