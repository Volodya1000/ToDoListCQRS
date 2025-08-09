using AutoMapper;
using ToDoListCQRS.Domain.Entities;
using ToDoListCQRS.Domain.ValueObjects;
using ToDoListCQRS.Persistence.Entities;

namespace ToDoListCQRS.Persistence.Mapping;

public class ToDoItemProfile : Profile
{
    public ToDoItemProfile()
    {
        CreateMap<TitleValueObject, string>().ConvertUsing(src => src.Value);

        CreateMap<string, TitleValueObject>().ConvertUsing(src => new TitleValueObject(src));

        CreateMap<ToDoItemEntity, ToDoItem>()
            .ConstructUsing(src => ToDoItem.Create(src.Id, new TitleValueObject(src.Title)))
            .AfterMap((src, dest) =>
            {
                dest.UpdateContent(src.Content);
                if (src.IsDone)
                    dest.MarkAsDone();
            });

        CreateMap<ToDoItem, ToDoItemEntity>()
           .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
           .ForMember(d => d.Title, opt => opt.MapFrom(s => s.Title.Value))
           .ForMember(d => d.Content, opt => opt.MapFrom(s => s.Content))
           .ForMember(d => d.IsDone, opt => opt.MapFrom(s => s.IsDone));
    }
}
