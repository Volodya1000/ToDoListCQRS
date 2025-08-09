using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoListCQRS.Application.Comands;
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

    [HttpPost]
    [ProducesResponseType(typeof(ToDoItem), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CreateToDoCommand command, CancellationToken cst)
    {
        var id=await _mediator.Send(command, cst);

        return Ok(id);
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
}
