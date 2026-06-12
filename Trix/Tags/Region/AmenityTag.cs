namespace CVMatrix.Maptrix.Trix.Tags.Region;

public sealed record AmenityTag : IWayTag
{
    public required EValue? Value { get; init; }
    public required string RawValue { get; init; }

    public enum EValue
    {
        Parking,
        Fuel,
        School,
        Church,
        Restaurant
    }
}