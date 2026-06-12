namespace CVMatrix.Maptrix.OverpassAPI.Model.Clean;

public readonly struct CleanNodeId(ulong value) : IEquatable<CleanNodeId>
{
    public bool Equals(CleanNodeId other)
    {
        return Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        return obj is CleanNodeId other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public readonly ulong Value = value;
    public static bool operator ==(CleanNodeId left, CleanNodeId right) => left.Value == right.Value;
    public static bool operator !=(CleanNodeId left, CleanNodeId right) => left.Value != right.Value;
    public static implicit operator ulong(CleanNodeId id) => id.Value;
    public static implicit operator CleanNodeId(ulong value) => new(value);
}