namespace CVMatrix.DropOffDefense.SLib.OverpassAPI.Model.Clean;

public record CleanRelationMember
{
    public static CleanRelationMember FromRawMember(Raw.RawRelationMember member)
    {
        return new()
        {
            Ref = member.Ref,
            Type = member.Type switch
            {
                "node" => ERelationType.Node,
                "way" => ERelationType.Way,
                "relation" => ERelationType.Relation,
                _ => throw new NotSupportedException()
            },
            Role = member.Role
        };
    }

    public required ERelationType Type { get; init; }
    /// <summary>
    /// "outer" for outer bounds, "inner" for inner subtractive bounds.<br></br>
    /// (idk about others)
    /// </summary>
    public required string Role { get; init; }
    public required ulong Ref { get; init; }

    public enum ERelationType
    {
        Node,
        Way,
        Relation
    }

    public virtual bool Equals(CleanRelationMember? other)
    {
        return other is not null && Ref == other.Ref && Type == other.Type;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Type, Ref);
    }
}