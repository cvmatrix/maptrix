namespace CVMatrix.DropOffDefense.SLib.Core.Behaviors;

using Handles;
using BehaviorContexts.TravelNode;
public interface ITravelNodeBehavior : IBehavior<ITravelNodeHandle, Messages.ETravelNodeMessage, Actions.ETravelNodeAction>
{
    public IEnumerable<Actions.ETravelNodeAction> OnTravelerPass(IOnTravelerPassContext context);
}