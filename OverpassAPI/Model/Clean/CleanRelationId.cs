namespace CVMatrix.Maptrix.OverpassAPI.Model.Clean;

public readonly struct CleanRelationId(ulong value) : IEquatable<CleanRelationId>
{
    public bool Equals(CleanRelationId other)
    {
        return Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        return obj is CleanRelationId other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public readonly ulong Value = value;
    public static bool operator ==(CleanRelationId left, CleanRelationId right) => left.Value == right.Value;
    public static bool operator !=(CleanRelationId left, CleanRelationId right) => left.Value != right.Value;
    public static implicit operator ulong(CleanRelationId id) => id.Value;
    public static implicit operator CleanRelationId(ulong value) => new(value);
}