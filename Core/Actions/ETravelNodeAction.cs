namespace CVMatrix.DropOffDefense.SLib.Core.Actions;

using Handles;

public abstract record ETravelNodeAction : IAction
{
    public sealed record Block(ITravelWayHandle Way) : ETravelNodeAction;

    public sealed record Unblock(ITravelWayHandle Way) : ETravelNodeAction;
}