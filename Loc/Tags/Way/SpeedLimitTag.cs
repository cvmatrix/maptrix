namespace CVMatrix.DropOffDefense.SLib.Loc.Tags.Way;

public sealed record SpeedLimitTag : IWayTag
{
    /// <summary>
    /// Speed limit in MPH.
    /// </summary>
    public required int Value { get; init; }
    public required string RawValue { get; init; }
}