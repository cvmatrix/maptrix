namespace CVMatrix.DropOffDefense.SLib.Core.BehaviorContexts;

public interface IOnTickContext<out THandle> : IBehaviorContext<THandle>
{
    public TimeSpan TimeStep { get; }
}