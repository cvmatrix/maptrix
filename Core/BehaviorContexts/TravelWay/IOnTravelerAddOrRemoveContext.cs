namespace CVMatrix.DropOffDefense.SLib.Core.BehaviorContexts.TravelWay;

using Handles;
public interface IOnTravelerAddOrRemoveContext : IBehaviorContext<ITravelNodeHandle>
{
    public ITravelerHandle Traveler { get; }
}