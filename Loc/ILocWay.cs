namespace CVMatrix.DropOffDefense.SLib.Loc;

public interface ILocWay : ILocMapElement, ILocTaggable<Tags.ILocWayTag>
{
    public float Distance { get; }
    public int WayConnectionId { get; }
    public IReadOnlyList<Coordinates> Path { get; }
    public ILocNode From { get; }
    public ILocNode To { get; }
    public ILocWay? AdjacentReverse { get; }
}