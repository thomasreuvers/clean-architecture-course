using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Exceptions;
using MediatR;

namespace CleanArchitecture.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest;

public class DeleteLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository) : IRequestHandler<DeleteLeaveRequestCommand>
{
    public async Task Handle(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = await leaveRequestRepository.GetByIdAsync(request.Id);

        if (leaveRequest == null)
        {
            throw new NotFoundException(nameof(Domain.LeaveRequest), request.Id);
        }
        
        await leaveRequestRepository.DeleteAsync(leaveRequest);
    }
}