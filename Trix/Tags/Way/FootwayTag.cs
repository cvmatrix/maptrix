namespace CVMatrix.Maptrix.Trix.Tags.Way;

/// <summary>
/// <i>Subtag of: <see cref="WayTypeTag.EValue.Footway"/></i>
/// </summary>
public sealed record FootwayTag : IWayTag
{
    public enum EValue
    {
        /// <summary>
        /// Pedestrian sidewalk.
        /// </summary>
        Sidewalk,
        /// <summary>
        /// Padestrian road-crossing.<br></br>
        /// <br></br>
        /// <i>Primary subtag: <see cref="CrossingTag"/></i>
        /// </summary>
        Crossing,
    }
    public required EValue? Value { get; init; }
    public required string RawValue { get; init; }
}