namespace CVMatrix.Maptrix.OverpassAPI.Model.Clean;

public record CleanData
{
    public static CleanData FromRawResponse(Raw.RawResponse response)
    {
        var initSize = response.Elements.Count / 2;
        Dictionary<CleanNodeId, CleanNode> nodes = new(initSize);
        Dictionary<CleanWayId, CleanWay> ways = new(initSize);
        Dictionary<CleanRelationId, CleanRelation> relations = new(initSize);
        foreach (var element in response.Elements)
            switch (element.Type)
            {
                case "node":
                    nodes[element.Id] = CleanNode.FromRawElement(element);
                    break;
                case "way":
                    ways[element.Id] = CleanWay.FromRawElement(element);
                    break;
                case "relation":
                    relations[element.Id] = CleanRelation.FromRawElement(element);
                    break;
            }

        return new()
        {
            Nodes = nodes,
            Ways = ways,
            Relations = relations
        };
    }

    public required IReadOnlyDictionary<CleanNodeId, CleanNode> Nodes { get; init; }
    public required IReadOnlyDictionary<CleanRelationId, CleanRelation> Relations { get; init; }
    public required IReadOnlyDictionary<CleanWayId, CleanWay> Ways { get; init; }
}