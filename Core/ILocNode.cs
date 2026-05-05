namespace CVMatrix.DropOffDefense.SLib.Core;

public interface ILocNode
{
    public Coordinates Position { get; }
    public IReadOnlySet<ILocWay> Incoming { get; }
    public IReadOnlySet<ILocWay> Outgoing { get; }
}