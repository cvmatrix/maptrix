namespace CVMatrix.DropOffDefense.SLib.Core.Behaviors;
using Handles;
public interface ITravelNodeBehavior : IBehavior<ITravelNodeHandle, Messages.ETravelNodeMessage>
{
    public void OnTravelerPass(ITravelerHandle traveler, ITravelWayHandle from, ITravelWayHandle to);
}