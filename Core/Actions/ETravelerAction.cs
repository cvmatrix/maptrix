namespace CVMatrix.DropOffDefense.SLib.Core.Actions;

using Handles;
using TickContext = BehaviorContexts.IOnTickContext<Handles.ITravelerHandle>;
using AnyContext = BehaviorContexts.IBehaviorContext<Handles.ITravelerHandle>;
public abstract record ETravelerAction(AnyContext _) : IAction
{
}