namespace ToDoListCQRS.Persistence.Entities;

public class ToDoItemEntity
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
}
