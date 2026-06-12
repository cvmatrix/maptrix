namespace CVMatrix.Maptrix.Trix.Tags.Poi;

public sealed record NameTag : IPoiTag
{
    public string Value => RawValue;
    public required string RawValue { get; init; }
}