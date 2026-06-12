namespace CVMatrix.Maptrix.Trix.Tags.Region;

public sealed record LeisureTag : IWayTag
{
    public required EValue? Value { get; init; }
    public required string RawValue { get; init; }

    public enum EValue
    {
        /// <summary>
        ///     Area designed for practising sport, normally designated with appropriate markings.
        /// </summary>
        SportPitch,
        Garden,
        Playground,
        DogPark
    }
}