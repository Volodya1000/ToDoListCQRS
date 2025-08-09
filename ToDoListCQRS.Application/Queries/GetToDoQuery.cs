using AutoMapper;
using MediatR;
using ToDoListCQRS.Application.Dtos;
using ToDoListCQRS.Domain.Interfaces.Repositories.IToDoRepository;

namespace ToDoListCQRS.Application.Queries;

public record GetToDoQuery(Guid Id):IRequest<ToDoItemDto>;

public class GetToDoQueryHandler : IRequestHandler<GetToDoQuery, ToDoItemDto>
{
    private readonly IToDoRepository _toDoRepository;
    private readonly IMapper _mapper;

    public GetToDoQueryHandler(IToDoRepository toDoRepository, IMapper mapper)
    {
        _toDoRepository = toDoRepository;
        _mapper = mapper;
    }
    public async Task<ToDoItemDto> Handle(GetToDoQuery request, CancellationToken cst=default)
    {
        var toDoItem = await _toDoRepository.GetByIdAsync(request.Id, cst)
          ?? throw new KeyNotFoundException($"ToDo item with Id {request.Id} not found");

        return _mapper.Map<ToDoItemDto>(toDoItem);
    }
}
