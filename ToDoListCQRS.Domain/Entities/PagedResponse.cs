namespace ToDoListCQRS.Domain.Entities;

public record PagedResponse<T>(
    IReadOnlyList<T> Data,
    int TotalRecords,
    int PageNumber,
    int PageSize)
{
    public int TotalPages => PageSize == 0 ? 0 : (int)Math.Ceiling((decimal)TotalRecords / PageSize);

    public bool HasPreviousPage => PageNumber > 1;

    public bool HasNextPage => PageNumber < TotalPages;
}
