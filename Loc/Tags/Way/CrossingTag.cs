namespace CVMatrix.DropOffDefense.SLib.Loc.Tags.Way;

public sealed record CrossingTag : IWayTag
{
    public enum EValue
    {
        /// <summary>
        /// Pedestrian crossings without road markings or traffic signals.
        /// </summary>
        Unmarked,
        /// <summary>
        /// Generic crossing with no traffic-signals of any type, just road markings.
        /// </summary>
        Marked,
        /// <summary>
        /// Traffic light controlled pedestrian crossing.
        /// </summary>
        TrafficLight,
    }
    public required EValue? Value { get; init; }
    public required string RawValue { get; init; }
}