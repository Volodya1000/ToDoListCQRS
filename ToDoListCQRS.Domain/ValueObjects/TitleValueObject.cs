namespace ToDoListCQRS.Domain.ValueObjects;

public sealed record TitleValueObject : StringValueObject
{
    public const int MIN_LENGTH = 4;
    public const int MAX_LENGTH = 15;

    public TitleValueObject(string value)
        : base(value, MIN_LENGTH, MAX_LENGTH)
    {
    }
}