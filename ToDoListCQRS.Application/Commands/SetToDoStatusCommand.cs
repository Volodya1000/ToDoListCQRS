using MediatR;
using ToDoListCQRS.Domain.Interfaces.Repositories.IToDoRepository;

namespace ToDoListCQRS.Application.Commands;

public record SetToDoStatusCommand(Guid Id, bool IsDone) : IRequest<Unit>;

public class SetToDoStatusCommandHandler : IRequestHandler<SetToDoStatusCommand, Unit>
{
    private readonly IToDoRepository _toDoRepository;

    public SetToDoStatusCommandHandler(IToDoRepository toDoRepository)
    {
        _toDoRepository = toDoRepository;
    }

    public async Task<Unit> Handle(SetToDoStatusCommand request, CancellationToken cst = default)
    {
        var toDoItem = await _toDoRepository.GetByIdAsync(request.Id, cst)
            ?? throw new KeyNotFoundException($"ToDo item with Id {request.Id} not found");

        if (request.IsDone)
            toDoItem.MarkAsDone();
        else
            toDoItem.MarkAsNotDone();

        await _toDoRepository.UpdateAsync(toDoItem, cst);

        return Unit.Value;
    }
}
