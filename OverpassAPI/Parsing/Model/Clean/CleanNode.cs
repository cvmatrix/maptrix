namespace CVMatrix.DropOffDefense.SLib.OverpassAPI.Parsing.Model.Clean;

public record CleanNode : ICleanElement
{
    private CleanNode()
    {

    }

    [Flags]
    public enum EKnownTags
    {

    }
    public required ulong Id { get; init; }
    public required Coordinates Coordinates { get; init; }
    public required IReadOnlyDictionary<string, string> AllTags { get; init; }
    public required EKnownTags KnownTags { get; init; }

    /// <summary>
    /// Assumes <paramref name="element"/> is a node.
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    public static CleanNode FromRawElement(Raw.RawElement element)
    {
        return new()
        {
            Id = element.Id,
            Coordinates = new((double)element.Lat!, (double)element.Lon!),
            AllTags = element.Tags,
            KnownTags = GetKnownTags(element.Tags),
        };
    }

    private static EKnownTags GetKnownTags(IEnumerable<KeyValuePair<string, string>> tags)
    {
        throw new NotImplementedException();
    }
    public virtual bool Equals(CleanNode? other)
    {
        return other is not null && Id == other.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}