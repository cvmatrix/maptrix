namespace CVMatrix.DropOffDefense.SLib.Loc;

public interface ILocWay : ILocMapElement, ILocTaggable<Tags.ILocWayTag>
{
    public float Distance { get; }
    public int WayConnectionId { get; }
    public IReadOnlyList<LocCoordinates> Path { get; }
    public ILocIntersection From { get; }
    public ILocIntersection To { get; }
    public ILocWay? AdjacentReverse { get; }
}