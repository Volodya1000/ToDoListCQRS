using MediatR;
using MediatR.Pipeline;
using ToDoListCQRS.Domain.Interfaces.Repositories.IToDoRepository;

namespace ToDoListCQRS.Application.Comands;

public record UpdateToDoCommand(Guid Id,string Title, string Content):IRequest<Unit>;

public class UpdateToDoCommandHandler : IRequestHandler<UpdateToDoCommand, Unit>
{
    private readonly IToDoRepository _toDoRepository;

    public UpdateToDoCommandHandler(IToDoRepository toDoRepository)
    {
        _toDoRepository = toDoRepository;
    }

    public async Task<Unit> Handle(UpdateToDoCommand request, CancellationToken cst = default)
    {
        var toDoItem = await _toDoRepository.GetByIdAsync(request.Id,cst)
            ??   throw new KeyNotFoundException($"ToDo item with Id {request.Id} not found"); 

        toDoItem.UpdateTitle(request.Title);
        toDoItem.UpdateContent(request.Content);

        await _toDoRepository.UpdateAsync(toDoItem,cst);

        return Unit.Value;
    }
}
