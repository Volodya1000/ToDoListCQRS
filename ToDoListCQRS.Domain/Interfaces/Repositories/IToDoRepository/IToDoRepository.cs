using ToDoListCQRS.Domain.Entities;

namespace ToDoListCQRS.Domain.Interfaces.Repositories.IToDoRepository;

public interface IToDoRepository
{
    Task AddAcync(ToDoItem item, CancellationToken cst = default);
    Task<PagedResponse<ToDoItem>> GetListAsync(
        int pageNumber,
        int pageSize,
        CancellationToken cst = default);
    Task UpdateAsync(ToDoItem item, CancellationToken cst = default);
    Task<ToDoItem?> GetByIdAsync(Guid Id, CancellationToken cst = default);
    Task DeleteAsync(ToDoItem item, CancellationToken cst = default);
}
