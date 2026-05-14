namespace CVMatrix.DropOffDefense.SLib.Loc.Tags.Way;

public sealed record AmenityTag : IWayTag
{
    public enum EValue
    {
        Parking,
        Fuel,
    }
    public required EValue? Value { get; init; }
    public required string RawValue { get; init; }
}