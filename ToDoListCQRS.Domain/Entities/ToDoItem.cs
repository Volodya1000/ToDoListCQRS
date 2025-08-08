namespace ToDoListCQRS.Domain.Entities;

public class ToDoItem
{
    public Guid Id { get;}

    public string Title { get; private set; }

    public string Content { get; private set; } = "";

    // Конструктор для создания нового объекта с новым Id
    private ToDoItem(string title)
    {
        Id = Guid.NewGuid();
        Title = title;
    }

    // Конструктор для восстановления из хранилища с заданным Id
    public ToDoItem(Guid id, string title, string content)
    {
        Id = id;
        Title = title;
        Content = content;
    }

    public static ToDoItem Create(string title)
    {
        return new ToDoItem(title);
    }
}
