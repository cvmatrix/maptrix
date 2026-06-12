namespace CVMatrix.Maptrix.Trix.Tags.Way;

public sealed record NameTag : IWayTag
{
    public required string RawValue { get; init; }
    public string Value => RawValue;
}