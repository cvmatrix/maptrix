namespace CVMatrix.DropOffDefense.SLib.Loc.Tags.Way;

public sealed record NameTag : IWayTag
{
    public string Value => RawValue;
    public required string RawValue { get; init; }
}