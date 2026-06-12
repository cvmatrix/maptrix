namespace CVMatrix.Maptrix.Loc.Tags.Region;

public sealed record ShopTag : IWayTag
{
    public enum EValue
    {
        Supermarket,
        Convenience,
        Clothing,
    }
    public required EValue? Value { get; init; }
    public required string RawValue { get; init; }
}