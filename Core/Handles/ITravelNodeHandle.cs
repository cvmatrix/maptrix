namespace CVMatrix.DropOffDefense.SLib.Core.Handles;

public interface ITravelNodeHandle : IHandle<Messages.ETravelNodeMessage, Stats.TravelNodeStats>
{
    public IReadOnlySet<ITravelWayHandle> Outgoing { get; }
    public IReadOnlySet<ITravelWayHandle> Incoming { get; }
    public IReadOnlySet<ITravelWayHandle> Blocked { get; }
}