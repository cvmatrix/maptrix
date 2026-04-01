namespace CVMatrix.DropOffDefense.SLib.Core.Behaviors;
using Handles;
public interface ITravelerBehavior : IBehavior<ITravelerHandle, Messages.ETravelerMessage, Actions.ETravelerAction>
{
    public IEnumerable<Actions.ETravelWayAction> OnWayTransfer(ITravelWayHandle from, ITravelWayHandle to, ITravelNodeHandle through);
}