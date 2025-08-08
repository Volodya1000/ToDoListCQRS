using AutoMapper;
using ToDoListCQRS.Application.Dtos;
using ToDoListCQRS.Domain.Entities;

namespace ToDoListCQRS.Application.Mapping.PagedResponce;

public class PagedResponceProfile : Profile
{
    public PagedResponceProfile()
    {
        CreateMap<PagedResponse<ToDoItem>, PagedResponse<ToDoItemDto>>()
            .ConvertUsing<PagedResponseConverter<ToDoItem, ToDoItemDto>>();
    }
}
