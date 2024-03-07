using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Exceptions;
using MediatR;

namespace CleanArchitecture.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;

public class GetLeaveTypeDetailsQueryHandler(
    IMapper mapper,
    ILeaveTypeRepository repository)
    : IRequestHandler<GetLeaveTypeDetailsQuery, LeaveTypeDetailsDto>
{
    public async Task<LeaveTypeDetailsDto> Handle(GetLeaveTypeDetailsQuery request, CancellationToken cancellationToken)
    {
        // Query the database
        var leaveType = await repository.GetByIdAsync(request.Id);

        if (leaveType == null)
        {
            throw new NotFoundException(nameof(Domain.LeaveType), request.Id);
        }
        
        // Map the results to a LeaveTypeDetailDto
        var data = mapper.Map<LeaveTypeDetailsDto>(leaveType);
        
        // Return the LeaveTypeDetailDto
        return data;
    }
}