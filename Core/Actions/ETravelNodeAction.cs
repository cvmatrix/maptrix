namespace CVMatrix.DropOffDefense.SLib.Core.Actions;

using Handles;
using BehaviorContexts.TravelNode;
using TickContext = BehaviorContexts.IOnTickContext<Handles.ITravelNodeHandle>;
using AnyContext = BehaviorContexts.IBehaviorContext<Handles.ITravelNodeHandle>;
public abstract record ETravelNodeAction(AnyContext _) : IAction
{
    public sealed record SetBlocked(AnyContext Context, ITravelWayHandle Way, bool Blocked) : ETravelNodeAction(Context);
    public sealed record ConsumeTraveler(IOnTravelerPassContext Context) : ETravelNodeAction(Context);
    public sealed record SpawnTraveler(AnyContext Context, Stats.TravelerStats Stats, ITravelWayHandle OnWay) : ETravelNodeAction(Context);
}