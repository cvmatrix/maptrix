namespace CVMatrix.Maptrix.Trix.Tags.Region;

public sealed record ShopTag : IWayTag
{
    public required EValue? Value { get; init; }
    public required string RawValue { get; init; }

    public enum EValue
    {
        Supermarket,
        Convenience,
        Clothing
    }
}