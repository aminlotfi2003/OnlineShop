namespace OnlineShop.SharedKernel.Entities;

public abstract class ValueObject : IEquatable<ValueObject>
{
    protected abstract IEnumerable<object?> GetEqualityComponents();

    public override bool Equals(object? obj)
        => obj is ValueObject other && ValuesEqual(other);

    public bool Equals(ValueObject? other)
        => other is not null && ValuesEqual(other);

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            foreach (var component in GetEqualityComponents())
                hash = hash * 23 + (component?.GetHashCode() ?? 0);
            return hash;
        }
    }

    public static bool operator ==(ValueObject? a, ValueObject? b)
        => ReferenceEquals(a, b) || (a is not null && b is not null && a.Equals(b));

    public static bool operator !=(ValueObject? a, ValueObject? b) => !(a == b);

    private bool ValuesEqual(ValueObject other)
        => GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
}
