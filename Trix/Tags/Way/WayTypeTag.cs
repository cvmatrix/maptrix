namespace CVMatrix.Maptrix.Trix.Tags.Way;

public sealed record WayTypeTag : IWayTag
{
    public required EValue? Value { get; init; }
    public required string RawValue { get; init; }

    public enum EValue
    {
        /// <summary>
        ///     (Vehicle) road in a residential area.
        /// </summary>
        ResidentialRoad,

        /// <summary>
        ///     (Vehicle) road that provides access to a building, service station, beach, campsite, industrial estate, business
        ///     park, etc. <br></br>
        ///     <br></br>
        ///     <i>Primary subtag: <see cref="ServiceRoadTag" /></i>
        /// </summary>
        ServiceRoad,

        /// <summary>
        ///     Path mainly or exclusively for pedestrians. <br></br>
        ///     <br></br>
        ///     <i>Primary subtag: <see cref="FootwayTag" />.</i>
        /// </summary>
        Footway,

        /// <summary>
        ///     Minor land-access road like a farm or forest track.
        /// </summary>
        Track,

        /// <summary>
        ///     Generic path used by pedestrians, small vehicles, for animal riding or livestock walking. Not designated for use by
        ///     two-track vehicles.
        /// </summary>
        Path,

        /// <summary>
        ///     (Vehicle) public access road, non-residential.
        /// </summary>
        GeneralAccessRoad,

        /// <summary>
        ///     (Vehicle) road linking small settlements, or the local centres of a large town or city.
        /// </summary>
        TertiaryRoad,

        /// <summary>
        ///     (Vehicle) highway linking large towns. In cities, an arterial road of lesser importance.
        /// </summary>
        SecondaryRoad,

        /// <summary>
        ///     (Vehicle) major highway linking large towns. In cities, a major arterial road.
        /// </summary>
        PrimaryRoad,

        /// <summary>
        ///     (Vehicle) important road that is not a highway/freeway.
        /// </summary>
        TrunkRoad,

        /// <summary>
        ///     (Vehicle) road with very low speed limits and other pedestrian friendly traffic rules.
        /// </summary>
        LivingStreetRoad,

        /// <summary>
        ///     (Vehicle) link roads (sliproads / ramps) leading to and from a highway/freeway.
        /// </summary>
        HighwayLink,

        /// <summary>
        ///     (Vehicle) high capacity highways/freeways designed to safely carry fast motor traffic.
        /// </summary>
        HighwayRoad,

        /// <summary>
        ///     (Vehicle) link road (sliproad / ramp) leading to and from a trunk road.
        /// </summary>
        TrinkLinkRoad
    }
}