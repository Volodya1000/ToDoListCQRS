using AutoMapper;
using ToDoListCQRS.Application.Dtos;
using ToDoListCQRS.Domain.Entities;

namespace ToDoListCQRS.Application.Mapping;

public class ToDoProfile: Profile
{
    public ToDoProfile()
    {
        CreateMap<ToDoItem, ToDoItemDto>();
    }
}
