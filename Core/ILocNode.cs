namespace CVMatrix.DropOffDefense.SLib.Core;

public interface ILocNode : ILocMapElement, ILocTaggable<Tags.ILocNodeTag>
{
    public Coordinates Position { get; }
    public IReadOnlySet<ILocWay> Incoming { get; }
    public IReadOnlySet<ILocWay> Outgoing { get; }
}