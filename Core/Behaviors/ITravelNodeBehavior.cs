namespace CVMatrix.DropOffDefense.SLib.Core.Behaviors;

using Handles;

public interface ITravelNodeBehavior : IBehavior<ITravelNodeHandle, Messages.ETravelNodeMessage, Actions.ETravelNodeAction>
{
    public IEnumerable<Actions.ETravelNodeAction> OnTravelerPass(ITravelerHandle traveler, ITravelWayHandle from, ITravelWayHandle to);
}