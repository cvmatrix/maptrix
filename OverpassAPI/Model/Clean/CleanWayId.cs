namespace CVMatrix.DropOffDefense.SLib.OverpassAPI.Model.Clean;

public readonly struct CleanWayId(ulong value) : IEquatable<CleanWayId>
{
    public bool Equals(CleanWayId other)
    {
        return Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        return obj is CleanWayId other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public readonly ulong Value = value;
    public static bool operator ==(CleanWayId left, CleanWayId right) => left.Value == right.Value;
    public static bool operator !=(CleanWayId left, CleanWayId right) => left.Value != right.Value;
    public static implicit operator ulong(CleanWayId id) => id.Value;
    public static implicit operator CleanWayId(ulong value) => new(value);
}