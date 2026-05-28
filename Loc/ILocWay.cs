namespace CVMatrix.DropOffDefense.SLib.Loc;

public interface ILocWay : ILocMapElement, ILocTaggable<Tags.IWayTag>
{
    public float Distance { get; }
    public int WayConnectionId { get; }
    public IReadOnlyList<ILocCoordinates> Path { get; }
    public ILocIntersection From { get; }
    public ILocIntersection To { get; }
    public ILocWay? AdjacentReverse { get; }
}