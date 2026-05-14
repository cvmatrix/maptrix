namespace CVMatrix.DropOffDefense.SLib.Loc;

public interface ILocIntersection : ILocMapElement, ILocTaggable<Tags.ILocIntersectionTag>
{
    public LocCoordinates Position { get; }
    public IReadOnlySet<ILocWay> Incoming { get; }
    public IReadOnlySet<ILocWay> Outgoing { get; }
}