namespace CVMatrix.Maptrix.OverpassAPI.Model.Raw;

public class RawRelationMember
{
    public string Role { get; set; } = string.Empty;
    public required string Type { get; set; }
    public required ulong Ref { get; set; }
}