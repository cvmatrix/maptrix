namespace CVMatrix.DropOffDefense.SLib.Core.Actions;

using Handles;
using TickContext = BehaviorContexts.IOnTickContext<Handles.ITravelWayHandle>;
using AnyContext = BehaviorContexts.IBehaviorContext<Handles.ITravelWayHandle>;
public abstract record ETravelWayAction(AnyContext _) : IAction
{
    public sealed record SetImpeded(AnyContext Context, bool Impeded) : ETravelWayAction(Context);

}