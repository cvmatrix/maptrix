namespace CVMatrix.DropOffDefense.SLib.Loc.Tags.Region;

public sealed record NameTag : IRegionTag
{
    public string Value => RawValue;
    public required string RawValue { get; init; }
}