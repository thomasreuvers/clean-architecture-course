using CleanArchitecture.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;
using CleanArchitecture.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;
using CleanArchitecture.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest;
using CleanArchitecture.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;
using CleanArchitecture.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail;
using CleanArchitecture.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class LeaveRequestsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<LeaveRequestListDto>>> Get(bool isLoggedInUser = false)
    {
        var leaveRequests = await mediator.Send(new GetLeaveRequestListQuery());
        return Ok(leaveRequests);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<LeaveRequestListDto>> Get(int id)
    {
        var leaveRequest = await mediator.Send(new GetLeaveRequestDetailQuery { Id = id });
        return Ok(leaveRequest);
    }
    
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Post(CreateLeaveRequestCommand command)
    {
        var response = await mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { id = response }, response);
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Put(UpdateLeaveRequestCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }
    
    [HttpPut]
    [Route("CancelRequest")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> CancelRequest(CancelLeaveRequestCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }
    
    [HttpPut]
    [Route("UpdateApproval")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> UpdateApproval(UpdateLeaveRequestCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }
    
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Delete(int id)
    {
        await mediator.Send(new DeleteLeaveRequestCommand { Id = id });
        return NoContent();
    }
}