namespace CVMatrix.DropOffDefense.SLib.OverpassAPI.Parsing.Model.Clean;

public class CleanWay : ICleanElement
{
    private CleanWay() { }
    [Flags]
    public enum EKnownTags
    {

    }
    public required ulong Id { get; init; }
    public required IReadOnlyList<ulong> Nodes { get; init; }
    public required IReadOnlyDictionary<string, string> AllTags { get; init; }
    public required EKnownTags KnownTags { get; init; }

    /// <summary>
    /// Assumes <paramref name="element"/> is a way.
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    public static CleanWay FromRawElement(Raw.RawElement element)
    {
        return new CleanWay()
        {
            Id = element.Id,
            Nodes = element.Nodes!,
            AllTags = element.Tags,
            KnownTags = GetKnownTags(element.Tags),
        };
    }
    private static EKnownTags GetKnownTags(IEnumerable<KeyValuePair<string, string>> tags)
    {
        throw new NotImplementedException();
    }
    public virtual bool Equals(CleanWay? other)
    {
        return other is not null && Id == other.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}