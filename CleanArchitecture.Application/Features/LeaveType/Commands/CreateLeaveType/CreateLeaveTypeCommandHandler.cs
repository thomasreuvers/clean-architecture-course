using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Exceptions;
using MediatR;

namespace CleanArchitecture.Application.Features.LeaveType.Commands.CreateLeaveType;

public class CreateLeaveTypeCommandHandler(
    IMapper mapper,
    ILeaveTypeRepository leaveTypeRepository) 
    : IRequestHandler<CreateLeaveTypeCommand, int>
{
    public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new CreateLeaveTypeCommandValidator(leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Count != 0)
        {
            throw new BadRequestException("Invalid LeaveType", validationResult);
        }
        
        // Map the incoming data to domain entity object
        var leaveTypeToCreate = mapper.Map<Domain.LeaveType>(request);

        // Create the domain entity object
        await leaveTypeRepository.CreateAsync(leaveTypeToCreate);

        // Save the new domain entity object to the database and return the Id
        return leaveTypeToCreate.Id;
    }
}