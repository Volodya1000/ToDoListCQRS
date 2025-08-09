using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoListCQRS.Application.Comands;
using ToDoListCQRS.Application.Commands;
using ToDoListCQRS.Application.Dtos;
using ToDoListCQRS.Application.Queries;
using ToDoListCQRS.Domain.Entities;

namespace ToDoListCQRS.WebApi.Controllers;

[Route("[controller]")]
public class ToDoListController:ControllerBase
{
    private readonly IMediator _mediator;

    public ToDoListController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ToDoItemDto),StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(Guid id, CancellationToken cst)
    {
        var command = new GetToDoQuery(id);
        var res=await _mediator.Send(command, cst);

        return Ok(res);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CreateToDoCommand command, CancellationToken cst)
    {
        var id=await _mediator.Send(command, cst);

        return CreatedAtAction(nameof(Get),new { id=id},id);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] GetToDoListQuery query, CancellationToken cst)
    {
        var rez = await _mediator.Send(query, cst);

        return Ok(rez);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Update([FromBody]UpdateToDoCommand command, CancellationToken cst)
    {
        await _mediator.Send(command, cst);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cst)
    {
        var command = new DeleteToDoCommand(id);
        await _mediator.Send(command, cst);

        return NoContent();
    }

    [HttpPatch("{id}/done")]
    public async Task<IActionResult> MarkAsDone(Guid id, CancellationToken cst)
    {
        var command = new SetToDoStatusCommand(id, true);
        await _mediator.Send(command, cst);
        return NoContent();
    }

    [HttpPatch("{id}/undone")]
    public async Task<IActionResult> MarkAsNotDone(Guid id, CancellationToken cst)
    {
        var command = new SetToDoStatusCommand(id, false);
        await _mediator.Send(command, cst);
        return NoContent();
    }
}