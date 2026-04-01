namespace CVMatrix.DropOffDefense.SLib.Core.Stats;

public record TravelerStats
{
    /// <summary>
    /// In speed/sec.
    /// </summary>
    public required double Acceleration { get; init; }
    /// <summary>
    ///  In dist/sec.
    /// </summary>
    public required double MaxSpeed { get; init; }
    public required IReadOnlySet<ERoadType> RoadCompatibility { get; init; }
}