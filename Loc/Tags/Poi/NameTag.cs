namespace CVMatrix.Maptrix.Loc.Tags.Poi;

public sealed record NameTag : IPoiTag
{
    public string Value => RawValue;
    public required string RawValue { get; init; }
}