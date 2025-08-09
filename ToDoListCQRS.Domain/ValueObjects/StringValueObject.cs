using System.Text.RegularExpressions;

namespace ToDoListCQRS.Domain.ValueObjects;

public abstract class StringValueObject
{
    public string Value { get; }

    private static Regex CreateDefaultRegex(int minLength, int maxLength) =>
        new($@"^[\p{{L}}\p{{M}}\p{{N}}]{{{minLength},{maxLength}}}$", RegexOptions.Singleline | RegexOptions.Compiled);

    protected StringValueObject(string value, int minLength, int maxLength)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(nameof(value), "Value cannot be null or whitespace");

        if (value.Length < minLength || value.Length > maxLength)
            throw new ArgumentException($"Value must be between {minLength} and {maxLength} characters", nameof(value));

        var validationRegex = CreateDefaultRegex(minLength, maxLength);
        if (!validationRegex.IsMatch(value))
            throw new ArgumentException("Value does not match required pattern", nameof(value));

        Value = value;
    }

    public override string ToString() => Value;

    public virtual bool Equals(StringValueObject? other)
    {
        if (ReferenceEquals(this, other)) return true;
        if (other is null) return false;
        if (GetType() != other.GetType()) return false;

        return string.Equals(Value, other.Value, StringComparison.Ordinal);
    }

    public override int GetHashCode() => HashCode.Combine(GetType(), Value);
}

