namespace CVMatrix.DropOffDefense.SLib.OverpassAPI.Model.Clean;

public record CleanData
{
    public required IReadOnlyDictionary<ulong, CleanNode> Nodes { get; init; }
    public required IReadOnlyDictionary<ulong, CleanWay> Ways { get; init; }
    public required IReadOnlyDictionary<ulong, CleanRelation> Relations { get; init; }

    public static CleanData FromRawResponse(Raw.RawResponse response)
    {
        var initSize = response.Elements.Count / 2;
        Dictionary<ulong, CleanNode> nodes = new(initSize);
        Dictionary<ulong, CleanWay> ways = new(initSize);
        Dictionary<ulong, CleanRelation> relations = new(initSize);
        foreach (var element in response.Elements)
        {
            switch (element.Type)
            {
                case "node":
                    nodes.Add(element.Id, CleanNode.FromRawElement(element));
                    break;
                case "way":
                    ways.Add(element.Id, CleanWay.FromRawElement(element));
                    break;
                case "relation":
                    relations.Add(element.Id, CleanRelation.FromRawElement(element));
                    break;
            }
        }

        return new()
        {
            Nodes = nodes,
            Ways = ways,
            Relations = relations,
        };
    }
}