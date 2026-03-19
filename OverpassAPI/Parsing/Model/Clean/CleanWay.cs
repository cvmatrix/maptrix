namespace CVMatrix.DropOffDefense.SLib.OverpassAPI.Parsing.Model.Clean;

public class CleanWay : ICleanElement
{
    private CleanWay() { }

    public required ulong Id { get; init; }
    public required IReadOnlyList<ulong> Nodes { get; init; }
    public required IReadOnlyDictionary<string, string> Tags { get; init; }

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
            Tags = element.Tags,
        };
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