using MediatR;
using ToDoListCQRS.Domain.Entities;
using ToDoListCQRS.Domain.Interfaces.Repositories.IToDoRepository;
using ToDoListCQRS.Domain.ValueObjects;

namespace ToDoListCQRS.Application.Comands;

public record CreateToDoCommand(string Title) : IRequest<Guid>;

public class CreateToDoCommandHandler : IRequestHandler<CreateToDoCommand, Guid>
{
    private readonly IToDoRepository _toDoRepository;

    public CreateToDoCommandHandler(IToDoRepository toDoRepository)
    {
        _toDoRepository = toDoRepository;
    }

    public async Task<Guid> Handle(CreateToDoCommand request, CancellationToken cst)
    {
        var toDoItem = ToDoItem.Create(Guid.NewGuid(), new TitleValueObject(request.Title));

        await _toDoRepository.AddAcync(toDoItem,cst);

        return toDoItem.Id;
    }
}
