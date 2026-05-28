namespace CVMatrix.DropOffDefense.SLib.Loc;

public interface ILocIntersection : ILocMapElement, ILocTaggable<Tags.IIntersectionTag>
{
    public ILocCoordinates Position { get; }
    public IReadOnlySet<ILocWay> Incoming { get; }
    public IReadOnlySet<ILocWay> Outgoing { get; }
}