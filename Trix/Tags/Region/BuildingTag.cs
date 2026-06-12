namespace CVMatrix.Maptrix.Trix.Tags.Region;

public sealed record BuildingTag : IWayTag
{
    public enum EValue
    {
        Unspecified,
        // DEV: include 'detached'
        House,
        Residential,
        Apartments,
        Commercial,
        Industrial,
        Retail,
    }
    public required EValue? Value { get; init; }
    public required string RawValue { get; init; }
}