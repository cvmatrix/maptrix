namespace CVMatrix.DropOffDefense.SLib.OverpassAPI.Parsing.Model.Clean;

public record CleanRelation : ICleanElement
{
    public required ulong Id { get; init; }
    public required IReadOnlyList<CleanRelationMember> Members { get; init; }
    public required IReadOnlyDictionary<string, string> Tags { get; init; }

    /// <summary>
    /// Assumes <paramref name="element"/> is a relation.
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    public static CleanRelation FromRawElement(Raw.RawElement element)
    {
        return new()
        {
            Id = element.Id,
            Members = element.Members!.Select(CleanRelationMember.FromRawMember).ToArray(),
            Tags = element.Tags,
        };
    }

    public virtual bool Equals(CleanRelation? other)
    {
        return other is not null && Id == other.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}