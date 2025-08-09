using MediatR;
using ToDoListCQRS.Domain.Interfaces.Repositories.IToDoRepository;

namespace ToDoListCQRS.Application.Commands;

public record DeleteToDoCommand(Guid Id):IRequest<Unit>;

public class DeleteToDoCommandHandler : IRequestHandler<DeleteToDoCommand, Unit>
{
    private readonly IToDoRepository _toDoRepository;

    public DeleteToDoCommandHandler(IToDoRepository toDoRepository)
    {
        _toDoRepository = toDoRepository;
    }

    public async Task<Unit> Handle(DeleteToDoCommand request, CancellationToken cst=default)
    {
        var toDoItem = await _toDoRepository.GetByIdAsync(request.Id, cst)
            ?? throw new KeyNotFoundException($"ToDo item with Id {request.Id} not found");

        await _toDoRepository.DeleteAsync(toDoItem, cst);

        return Unit.Value;
    }
}
