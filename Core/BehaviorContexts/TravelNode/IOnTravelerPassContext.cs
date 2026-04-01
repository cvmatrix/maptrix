namespace CVMatrix.DropOffDefense.SLib.Core.BehaviorContexts.TravelNode;

using Handles;
public interface IOnTravelerPassContext : IBehaviorContext<ITravelNodeHandle>
{
    public ITravelerHandle Traveler { get; }
    public ITravelWayHandle FromWay { get; }
    public ITravelWayHandle ToWay { get; }
}