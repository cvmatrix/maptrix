namespace CVMatrix.Maptrix.Trix.Tags.Way;

/// <summary>
///     <i>Subtag of: <see cref="FootwayTag.EValue.Crossing" /></i>
/// </summary>
public sealed record CrossingTag : IWayTag
{
    public required EValue? Value { get; init; }
    public required string RawValue { get; init; }

    public enum EValue
    {
        /// <summary>
        ///     Pedestrian crossings without road markings or traffic signals.
        /// </summary>
        Unmarked,

        /// <summary>
        ///     Generic crossing with no traffic-signals of any type, just road markings.
        /// </summary>
        Marked,

        /// <summary>
        ///     Traffic light controlled pedestrian crossing.
        /// </summary>
        TrafficLight
    }
}