namespace CVMatrix.Maptrix.Trix.Tags.Poi;

public sealed record ShopTag : IPoiTag
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