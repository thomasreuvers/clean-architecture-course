using MediatR;

namespace CleanArchitecture.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest;

public class DeleteLeaveRequestCommand : IRequest
{
    public int Id { get; set; }
}