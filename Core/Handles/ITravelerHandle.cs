namespace CVMatrix.DropOffDefense.SLib.Core.Handles;

public interface ITravelerHandle : IHandle<Messages.ETravelerMessage, Stats.TravelerStats>
{
    public Coordinates Position { get; }
    public double DistanceAlongWay { get; }
    public ITravelNodeHandle FinalTarget { get; }
    public ITravelWayHandle Traveling { get; }
}