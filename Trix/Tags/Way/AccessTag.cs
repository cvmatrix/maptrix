namespace CVMatrix.Maptrix.Trix.Tags.Way;

public sealed record AccessTag : IWayTag
{
    public enum EValue
    {
        /// <summary>
        /// Not to be used by the public.
        /// </summary>
        Private,
        /// <summary>
        /// Access requires a permit, ordinarily granted.
        /// </summary>
        Permit,
    }
    public required EValue? Value { get; init; }
    public required string RawValue { get; init; }
}