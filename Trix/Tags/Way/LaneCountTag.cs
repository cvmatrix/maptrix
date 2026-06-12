namespace CVMatrix.Maptrix.Trix.Tags.Way;

public sealed record LaneCountTag : IWayTag
{
    /// <summary>
    ///     The amount of lanes in this road.
    /// </summary>
    public required int Value { get; init; }

    public required string RawValue { get; init; }
}