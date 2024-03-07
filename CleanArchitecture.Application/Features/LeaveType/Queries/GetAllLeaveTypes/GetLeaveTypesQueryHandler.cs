using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using MediatR;

namespace CleanArchitecture.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

public class GetLeaveTypesQueryHandler(
    IMapper mapper,
    ILeaveTypeRepository leaveTypeRepository)
    : IRequestHandler<GetLeaveTypesQuery, List<LeaveTypeDto>>
{
    public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypesQuery request, CancellationToken cancellationToken)
    {
        // Query the database
        var leaveTypes = await leaveTypeRepository.GetAsync();
        
        // Map the results to a list of LeaveTypeDto
        var data = mapper.Map<List<LeaveTypeDto>>(leaveTypes);
        
        // Return the list of LeaveTypeDto
        return data;
    }
}