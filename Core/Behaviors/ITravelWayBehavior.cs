namespace CVMatrix.DropOffDefense.SLib.Core.Behaviors;
using Handles;
public interface ITravelWayBehavior : IBehavior<ITravelWayHandle, Messages.ETravelWayMessage, Actions.ETravelWayAction>
{
    public IEnumerable<Actions.ETravelWayAction> OnTravelerAdd(ITravelerHandle traveler, ITravelWayHandle from);
    public IEnumerable<Actions.ETravelWayAction> OnTravelerRemove(ITravelerHandle traveler, ITravelNodeHandle to);
}