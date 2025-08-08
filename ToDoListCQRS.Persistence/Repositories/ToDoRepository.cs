using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ToDoListCQRS.Domain.Entities;
using ToDoListCQRS.Domain.Interfaces.Repositories.IToDoRepository;
using ToDoListCQRS.Persistence.Entities;

namespace ToDoListCQRS.Persistence.Repositories;

public class ToDoRepository : IToDoRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;


    public ToDoRepository(ApplicationDbContext dbContext, IMapper mapper)
    {
        _context= dbContext;
        _mapper = mapper;
    }

    public  async Task AddAcync(ToDoItem item, CancellationToken cst = default)
    {
        var itemEntity = _mapper.Map<ToDoItemEntity>(item);
        await _context.ToDoItems.AddAsync(itemEntity, cst);
        await _context.SaveChangesAsync(cst);
    }

    public async Task<PagedResponse<ToDoItem>> GetListAsync(int pageNumber, 
                                                            int pageSize, 
                                                            CancellationToken cst = default)
    {
        var itemEntities = await _context.ToDoItems.AsNoTracking()
                                .Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync(cst);
        var totalRecords = await _context.ToDoItems.CountAsync(cst);

        var items = _mapper.Map<IReadOnlyList<ToDoItem>>(itemEntities);
        return new PagedResponse<ToDoItem>(items, totalRecords, pageNumber, pageSize);
    }
}
