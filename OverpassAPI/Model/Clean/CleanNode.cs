namespace CVMatrix.Maptrix.OverpassAPI.Model.Clean;

public record CleanNode
{
    /// <summary>
    ///     Assumes <paramref name="element" /> is a node.
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    public static CleanNode FromRawElement(Raw.RawElement element)
    {
        return new()
        {
            Id = element.Id,
            Longitude = (double)element.Lon!,
            Latitude = (double)element.Lat!,
            Tags = element.Tags
        };
    }

    public required CleanNodeId Id { get; init; }

    public required double Latitude { get; init; }
    public required double Longitude { get; init; }
    public required IReadOnlyDictionary<string, string> Tags { get; init; }

    public virtual bool Equals(CleanNode? other)
    {
        return other is not null && Id == other.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}