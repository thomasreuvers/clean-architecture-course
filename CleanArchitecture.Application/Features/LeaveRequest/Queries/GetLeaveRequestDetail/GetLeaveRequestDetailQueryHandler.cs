using AutoMapper;
using CleanArchitecture.Application.Contracts.Identity;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Exceptions;
using MediatR;

namespace CleanArchitecture.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail;

public class GetLeaveRequestDetailQueryHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper, IUserService userService) : IRequestHandler<GetLeaveRequestDetailQuery, LeaveRequestDetailsDto>
{
    public async Task<LeaveRequestDetailsDto> Handle(GetLeaveRequestDetailQuery request, CancellationToken cancellationToken)
    {
        var leaveRequest =
            mapper.Map<LeaveRequestDetailsDto>(await leaveRequestRepository.GetLeaveRequestWithDetails(request.Id));
        
        if (leaveRequest == null)
            throw new NotFoundException(nameof(LeaveRequest), request.Id);
        
        // Add Employee details as needed
        leaveRequest.Employee = await userService.GetEmployee(leaveRequest.RequestingEmployeeId);

        return leaveRequest;
    }
}