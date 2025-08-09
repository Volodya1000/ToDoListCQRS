using AutoMapper;
using ToDoListCQRS.Domain.Entities;
using ToDoListCQRS.Domain.ValueObjects;
using ToDoListCQRS.Persistence.Entities;

namespace ToDoListCQRS.Persistence.Mapping;

public class ToDoItemProfile : Profile
{
    public ToDoItemProfile()
    {
        CreateMap<ToDoItemEntity, ToDoItem>()
           .ConstructUsing(src => ToDoItem.Create(src.Id, new TitleValueObject(src.Title)))
           .AfterMap((src, dest) =>
           {
               dest.UpdateContent(src.Content);
               if (src.IsDone)
                   dest.MarkAsDone();
           });

        CreateMap<ToDoItem, ToDoItemEntity>();
    }
}
