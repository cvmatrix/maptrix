namespace CVMatrix.DropOffDefense.SLib.OverpassAPI.Parsing;

public class RawElement
{
    public required string Type { get; set; }
    public required int Id { get; set; }
    public double? Lat { get; set; }
    public double? Lon { get; set; }
    public List<int>? Nodes { get; set; }
    public List<RawRelationMember>? Members { get; set; }
    public List<Dictionary<string, string>> Tags { get; set; } = [];
}