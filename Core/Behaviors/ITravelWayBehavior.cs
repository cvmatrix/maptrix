namespace CVMatrix.DropOffDefense.SLib.Core.Behaviors;

using Handles;
using BehaviorContexts.TravelWay;
public interface ITravelWayBehavior : IBehavior<ITravelWayHandle, Messages.ETravelWayMessage, Actions.ETravelWayAction>
{
    public IEnumerable<Actions.ETravelWayAction> OnTravelerAdd(IOnTravelerAddOrRemoveContext context);
    public IEnumerable<Actions.ETravelWayAction> OnTravelerRemove(IOnTravelerAddOrRemoveContext context);
}