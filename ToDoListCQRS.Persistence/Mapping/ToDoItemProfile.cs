using AutoMapper;
using ToDoListCQRS.Domain.Entities;
using ToDoListCQRS.Persistence.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ToDoListCQRS.Persistence.Mapping;

public class ToDoItemProfile : Profile
{
    public ToDoItemProfile()
    {
        CreateMap<ToDoItem, ToDoItemEntity>();

        CreateMap<ToDoItemEntity, ToDoItem>()
            .ConstructUsing(src => new ToDoItem(src.Id, src.Title, src.Content));
    }
}
