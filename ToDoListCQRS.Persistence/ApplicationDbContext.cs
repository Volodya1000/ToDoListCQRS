using Microsoft.EntityFrameworkCore;
using ToDoListCQRS.Persistence.Configurations;
using ToDoListCQRS.Persistence.Entities;

namespace ToDoListCQRS.Persistence;

public class ApplicationDbContext : DbContext
{
    public DbSet<ToDoItemEntity> ToDoItems { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
         : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ToDoItemEntityConfiguration).Assembly);
    }
}
