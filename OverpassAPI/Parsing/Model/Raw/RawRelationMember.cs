namespace CVMatrix.DropOffDefense.SLib.OverpassAPI.Parsing.Model.Raw;

public class RawRelationMember
{
    public required string Type { get; set; }
    public required ulong Ref { get; set; }
    public string Role { get; set; } = string.Empty;
}