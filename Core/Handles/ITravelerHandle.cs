namespace CVMatrix.DropOffDefense.SLib.Core.Handles;

public interface ITravelerHandle : IHandle<Messages.ETravelerMessage, Stats.TravelerStats>
{
    public ITravelWayHandle Traveling { get; }
    public Coordinates Position { get; }
    public double DistanceAlongWay { get; }
    public ITravelNodeHandle FinalTarget { get; }
}