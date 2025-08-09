namespace ToDoListCQRS.Application.Dtos;

public record ToDoItemDto(Guid Id, string Title, string Content);