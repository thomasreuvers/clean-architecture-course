using AutoMapper;
using CleanArchitecture.Application.Contracts.Email;
using CleanArchitecture.Application.Contracts.Identity;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Application.Models.Email;
using MediatR;

namespace CleanArchitecture.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;

public class CreateLeaveRequestCommandHandler(
    IEmailSender emailSender,
    IMapper mapper,
    ILeaveTypeRepository leaveTypeRepository,
    ILeaveRequestRepository leaveRequestRepository,
    ILeaveAllocationRepository leaveAllocationRepository,
    IUserService userService)
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
        var employeeId = userService.UserId;
        
        // check on employee's allocation
        var allocation = await leaveAllocationRepository.GetUserAllocations(employeeId, request.LeaveTypeId);
        
        // if allocations aren't enough, return validation error with message
        if (allocation is null)
        {
            validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(nameof(
                request.LeaveTypeId), "You do not have an allocation for this leave type"));
            throw new BadRequestException("Invalid Leave Request", validationResult);
        }
        
        var daysRequested = (int)(request.EndDate - request.StartDate).TotalDays;
        if (daysRequested > allocation.NumberOfDays)
        {
            validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(nameof(
                request.EndDate), "You do not have enough days for this request"));
            throw new BadRequestException("Invalid Leave Request", validationResult);
        }
        
        // Create leave request
        var leaveRequest = mapper.Map<Domain.LeaveRequest>(request);
        leaveRequest.RequestingEmployeeId = employeeId;
        leaveRequest.DateRequested = DateTime.Now;
        await leaveRequestRepository.CreateAsync(leaveRequest);

        try
        {
            // Send confirmation email
            var email = new EmailMessage
            {
                To = string.Empty, /**/
                Body =
                    $"Your leave request for {request.StartDate:D} to {request.EndDate:D} has been submitted successfully.",
                Subject = "Leave Request Submitted"
            };
        
            await emailSender.SendEmail(email);
        }
        catch (Exception e)
        {
            // Log or handle error
        }
        
        return Unit.Value;
    }
}