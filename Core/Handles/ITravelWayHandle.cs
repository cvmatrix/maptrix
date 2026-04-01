namespace CVMatrix.DropOffDefense.SLib.Core.Handles;

public interface ITravelWayHandle : ITickable
{
    public double Distance { get; }
    public ITravelNodeHandle From { get; }
    public ITravelNodeHandle To { get; }
    public IReadOnlyList<Coordinates> Path { get; }
    /// <summary>
    /// Should be in order of distance travelled (least first).
    /// </summary>
    public IReadOnlyList<ITravelerHandle> Travelers { get; }
    public int LaneCount { get; }
    public ERoadType RoadType { get; }
}