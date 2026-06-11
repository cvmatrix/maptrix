namespace CVMatrix.DropOffDefense.SLib.OverpassAPI.Model.Clean;

public record CleanWay
{
    /// <summary>
    ///     Assumes <paramref name="element" /> is a way.
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    public static CleanWay FromRawElement(Raw.RawElement element)
    {
        return new()
        {
            Id = element.Id,
            Nodes = [..element.Nodes!],
            Tags = element.Tags
        };
    }

    public required IReadOnlyDictionary<string, string> Tags { get; init; }
    public required IReadOnlyList<CleanNodeId> Nodes { get; init; }
    public required CleanWayId Id { get; init; }

    public virtual bool Equals(CleanWay? other)
    {
        return other is not null && Id == other.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}