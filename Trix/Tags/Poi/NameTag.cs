namespace CVMatrix.Maptrix.Trix.Tags.Poi;

public sealed record NameTag : IPoiTag
{
    public required string RawValue { get; init; }
    public string Value => RawValue;
}