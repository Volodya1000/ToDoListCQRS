using AutoMapper;
using ToDoListCQRS.Domain.Entities;

namespace ToDoListCQRS.Application.Mapping.PagedResponce;

public class PagedResponseConverter<TSource, TDestination>
    : ITypeConverter<PagedResponse<TSource>, PagedResponse<TDestination>>
{
    public PagedResponse<TDestination> Convert(
        PagedResponse<TSource> source,
        PagedResponse<TDestination> destination,
        ResolutionContext context)
    {
        var items = context.Mapper.Map<IReadOnlyList<TDestination>>(source.Data);

        return new PagedResponse<TDestination>(
            items,
            source.TotalRecords,
            source.PageNumber,
            source.PageSize
        );
    }
}
