namespace CVMatrix.DropOffDefense.SLib.Core.Stats;

public record TravelerStats
{
    /// <summary>
    ///     In speed/sec.
    /// </summary>
    public required double Acceleration { get; init; }

    /// <summary>
    ///     In dist/sec.
    /// </summary>
    public required double MaxSpeed { get; init; }
    /// <summary>
    /// In distance units.
    /// Affects lane changing mechanics.
    /// </summary>
    public required double Size { get; init; }
    /// <summary>
    /// In distance units.
    /// How far ahead the traveler will look to slow down.
    /// </summary>
    public required double SightDistance { get; init; }
    /// <summary>
    /// Between 0 and 1 (1 for perfect effeciency).
    /// </summary>
    public required float DeccelerationEfficiency { get; init; }
    public required IReadOnlySet<ERoadType> RoadCompatibility { get; init; }
}