namespace CVMatrix.DropOffDefense.SLib.Core.Behaviors;

using Handles;
using BehaviorContexts.Traveler;
public interface ITravelerBehavior : IBehavior<ITravelerHandle, Messages.ETravelerMessage, Actions.ETravelerAction>
{
    public IEnumerable<Actions.ETravelWayAction> OnWayTransfer(IOnWayTransferContext context);
}