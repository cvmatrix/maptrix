namespace CVMatrix.DropOffDefense.SLib.Loc.Tags.Poi;

public sealed record AmenityTag : IPoiTag
{
    public enum EValue
    {
        Parking,
        Fuel,
        School,
        Church,
        Restaurant,
    }
    public required EValue? Value { get; init; }
    public required string RawValue { get; init; }
}