namespace CVMatrix.Maptrix.Trix.Tags.Region;

public sealed record NameTag : IRegionTag
{
    public required string RawValue { get; init; }
    public string Value => RawValue;
}