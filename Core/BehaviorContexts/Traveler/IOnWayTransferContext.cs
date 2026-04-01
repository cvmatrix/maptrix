namespace CVMatrix.DropOffDefense.SLib.Core.BehaviorContexts.Traveler;

using Handles;
public interface IOnWayTransferContext : IBehaviorContext<ITravelNodeHandle>
{
    public ITravelNodeHandle ThroughNode { get; }
    public ITravelWayHandle FromWay { get; }
    public ITravelWayHandle ToWay { get; }
}