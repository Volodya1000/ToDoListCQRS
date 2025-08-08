using AutoMapper;
using MediatR;
using ToDoListCQRS.Application.Dtos;
using ToDoListCQRS.Domain.Entities;
using ToDoListCQRS.Domain.Interfaces.Repositories.IToDoRepository;

namespace ToDoListCQRS.Application.Queries;

public record GetToDoListQuery(int PageNumber, int PageSize) :IRequest<PagedResponse<ToDoItemDto>>;


public class GetToDoListQueryHandler : IRequestHandler<GetToDoListQuery, PagedResponse<ToDoItemDto>>
{
    private readonly IToDoRepository _toDoRepository;
    private readonly IMapper _mapper;

    public GetToDoListQueryHandler(IToDoRepository toDoRepository, IMapper mapper)
    {
        _toDoRepository = toDoRepository;
        _mapper = mapper;
    }

    public async Task<PagedResponse<ToDoItemDto>> Handle(GetToDoListQuery request, CancellationToken cst=default)
    {
        var rez = await _toDoRepository.GetListAsync(request.PageNumber, request.PageSize, cst);
        return _mapper.Map<PagedResponse<ToDoItemDto>>(rez);
    }
}
