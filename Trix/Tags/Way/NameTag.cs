namespace CVMatrix.Maptrix.Trix.Tags.Way;

public sealed record NameTag : IWayTag
{
    public string Value => RawValue;
    public required string RawValue { get; init; }
}