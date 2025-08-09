using ToDoListCQRS.Domain.ValueObjects;

namespace ToDoListCQRS.Domain.Entities;

public class ToDoItem
{
    public Guid Id { get; }

    public TitleValueObject Title { get; private set; }

    public string Content { get; private set; } = "";

    public bool IsDone { get; private set; }

    private ToDoItem(Guid id, TitleValueObject title)
    {
        Id = id;
        Title = title;
        IsDone = false;
    }

    public static ToDoItem Create(Guid id, TitleValueObject title)
    {
        return new ToDoItem(id, title);
    }

    public void UpdateTitle(TitleValueObject newTitle)
    {
        Title = newTitle;
    }

    public void UpdateContent(string newContent)
    {
        Content = newContent;
    }

    public void MarkAsDone()
    {
        IsDone = true;
    }

    public void MarkAsNotDone()
    {
        IsDone = false;
    }
}
