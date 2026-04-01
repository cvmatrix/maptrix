namespace CVMatrix.DropOffDefense.SLib.OverpassAPI.Model.Clean;

public record CleanRelation : ICleanElement
{
    /// <summary>
    ///     Assumes <paramref name="element" /> is a relation.
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    public static CleanRelation FromRawElement(Raw.RawElement element)
    {
        return new()
        {
            Id = element.Id,
            Members = element.Members!.Select(CleanRelationMember.FromRawMember).ToArray(),
            Tags = element.Tags
        };
    }

    public required IReadOnlyDictionary<string, string> Tags { get; init; }
    public required IReadOnlyList<CleanRelationMember> Members { get; init; }
    public required ulong Id { get; init; }

    public virtual bool Equals(CleanRelation? other)
    {
        return other is not null && Id == other.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}