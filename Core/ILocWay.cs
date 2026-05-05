namespace CVMatrix.DropOffDefense.SLib.Core;

public interface ILocWay
{
    public float Distance { get; }
    public int WayConnectionId { get; }
    public IReadOnlyList<Coordinates> Path { get; }
    public ILocNode From { get; }
    public ILocNode To { get; }
    public ILocWay? TwoWay { get; }
    public Data.EWayType Type { get; }
}