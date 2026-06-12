namespace CVMatrix.Maptrix.Trix.Tags.Region;

public sealed record BuildingTag : IWayTag
{
    public required EValue? Value { get; init; }
    public required string RawValue { get; init; }

    public enum EValue
    {
        Unspecified,

        // DEV: include 'detached'
        House,
        Residential,
        Apartments,
        Commercial,
        Industrial,
        Retail
    }
}