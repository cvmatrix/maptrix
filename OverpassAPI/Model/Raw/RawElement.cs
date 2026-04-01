namespace CVMatrix.DropOffDefense.SLib.OverpassAPI.Model.Raw;

public class RawElement
{
    public Dictionary<string, string> Tags { get; set; } = [];
    public double? Lat { get; set; }
    public double? Lon { get; set; }
    public List<RawRelationMember>? Members { get; set; }
    public List<ulong>? Nodes { get; set; }
    public required string Type { get; set; }
    public required ulong Id { get; set; }
}