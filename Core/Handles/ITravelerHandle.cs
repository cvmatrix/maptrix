namespace CVMatrix.DropOffDefense.SLib.Core.Handles;

public interface ITravelerHandle : IHandle<Messages.ETravelerMessage, Stats.TravelerStats>
{
    public double DistanceAlongWay { get; }
    public ITravelNodeHandle FinalTarget { get; }
    public ITravelWayHandle Traveling { get; }
}