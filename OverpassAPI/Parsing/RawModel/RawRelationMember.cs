namespace CVMatrix.DropOffDefense.SLib.OverpassAPI.Parsing;

public class RawRelationMember
{
    public required string Type { get; set; }
    public required int Ref { get; set; }
    public string Role { get; set; } = string.Empty;
}