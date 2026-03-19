namespace CVMatrix.DropOffDefense.SLib.OverpassAPI.Parsing.Model.Clean;

public record CleanRelationMember
{
    private CleanRelationMember()
    {

    }

    public enum EKnownRole
    {
        None,
    }

    public enum ERelationType
    {
        Node,
        Way,
        Relation,
    }
    public required ERelationType Type { get; init; }
    public required ulong Ref { get; init; }
    public required EKnownRole? KnownRole { get; init; }
    public required string Role { get; init; }

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
            Role = member.Role,
            KnownRole = GetKnownRole(member.Role)
        };
    }

    private static EKnownRole? GetKnownRole(string role)
    {
        throw new NotImplementedException();
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