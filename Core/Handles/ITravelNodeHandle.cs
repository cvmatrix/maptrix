namespace CVMatrix.DropOffDefense.SLib.Core.Handles;

public interface ITravelNodeHandle : ITickable
{
    public IReadOnlySet<ITravelWayHandle> Outgoing { get; }
    public IReadOnlySet<ITravelWayHandle> Incoming { get; }
    public IReadOnlySet<ITravelWayHandle> Blocked { get; }
}