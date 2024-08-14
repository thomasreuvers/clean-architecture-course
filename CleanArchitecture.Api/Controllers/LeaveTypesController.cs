using CleanArchitecture.Application.Features.LeaveType.Commands.CreateLeaveType;
using CleanArchitecture.Application.Features.LeaveType.Commands.DeleteLeaveType;
using CleanArchitecture.Application.Features.LeaveType.Commands.UpdateLeaveType;
using CleanArchitecture.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using CleanArchitecture.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeaveTypesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<List<LeaveTypeDto>> Get()
    {
        var leaveTypes = await mediator.Send(new GetLeaveTypesQuery());
        return leaveTypes;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LeaveTypeDto>> Get(int id)
    {
        var leaveType = await mediator.Send(new GetLeaveTypeDetailsQuery(id));
        return Ok(leaveType);
    }
    
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Post(CreateLeaveTypeCommand command)
    {
        var response = await mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { id = response }, response);
    }
    
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Put(UpdateLeaveTypeCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Delete(int id)
    {
        await mediator.Send(new DeleteLeaveTypeCommand { Id = id});
        return NoContent();
    }
}