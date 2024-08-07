using CleanArchitecture.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using CleanArchitecture.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation;
using CleanArchitecture.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;
using CleanArchitecture.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using CleanArchitecture.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class LeaveAllocationsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<LeaveAllocationDto>>> Get(bool isLoggedInUser = false)
    {
        var leaveAllocations = await mediator.Send(new GetLeaveAllocationListQuery());
        return Ok(leaveAllocations);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<LeaveAllocationDto>> Get(int id)
    {
        var leaveAllocation = await mediator.Send(new GetLeaveAllocationDetailQuery { Id = id });
        return Ok(leaveAllocation);
    }
    
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Post(CreateLeaveAllocationCommand command)
    {
        var response = await mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { id = response }, response);
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Put(UpdateLeaveAllocationCommand command)
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
        await mediator.Send(new DeleteLeaveAllocationCommand { Id = id });
        return NoContent();
    }
}