namespace CVMatrix.Maptrix.Loc.Tags.Way;

/// <summary>
/// <i>Subtag of: <see cref="WayTypeTag.EValue.ServiceRoad"/></i>
/// </summary>
public sealed record ServiceRoadTag : IWayTag
{
    public enum EValue
    {
        /// <summary>
        /// A road is a driveway, typically leading to a residence or business.
        /// </summary>
        Driveway,
        /// <summary>
        /// A subordinated way in a parking lot between rows of parking spaces that vehicles use to drive into and out of the spaces.
        /// </summary>
        ParkingAisle,
        /// <summary>
        /// A service road usually located between properties for access to utilities.
        /// </summary>
        Alley,
    }
    public required EValue? Value { get; init; }
    public required string RawValue { get; init; }
}