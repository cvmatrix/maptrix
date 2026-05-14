namespace CVMatrix.DropOffDefense.SLib.Loc.Tags.Intersection;

// DEV: use the 'highway' and 'crossing' key.
public sealed record IntersectionTypeTag : IWayTag
{
    public enum EValue
    {
        /// <summary>
        /// Stop sign intersection.
        /// </summary>
        StopSign,
        /// <summary>
        /// Yield sign or similar road intersection.
        /// </summary>
        Yield,
        /// <summary>
        /// Street crossing for pedestrians, cyclists, or equestrians.
        /// </summary>
        PedestrianCrossing,
        /// <summary>
        /// Traffic-light controlled intersection.
        /// </summary>
        TrafficLight,
    }
    public required EValue? Value { get; init; }
    public required string RawValue { get; init; }
}