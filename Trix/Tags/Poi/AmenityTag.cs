namespace CVMatrix.Maptrix.Trix.Tags.Poi;

public sealed record AmenityTag : IPoiTag
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