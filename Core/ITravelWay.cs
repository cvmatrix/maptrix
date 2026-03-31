namespace CVMatrix.DropOffDefense.SLib.Core;

public interface ITravelWay : ITickable
{
    public double Distance { get; }
    public ITravelNode From { get; }
    public ITravelNode To { get; }
    public IReadOnlyList<Coordinates> Path { get; }
    public IReadOnlySet<ITraveler> Travelers { get; }
    public int LaneCount { get; }
}