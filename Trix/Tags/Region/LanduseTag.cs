namespace CVMatrix.Maptrix.Trix.Tags.Region;

public sealed record LanduseTag : IWayTag
{
    public enum EValue
    {
        Farmland,
        Residential,
        Grass,
        Forest,
        MiscFauna,
        Industrial,
        Religious,
    }
    public required EValue? Value { get; init; }
    public required string RawValue { get; init; }
}