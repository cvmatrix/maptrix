namespace CVMatrix.DropOffDefense.SLib.Core.Behaviors;

public interface IBehavior<in THandle, in TMessage, out TAction>
{
    public IEnumerable<TAction> OnRecieveMessage(TMessage message);
    public IEnumerable<TAction> OnTick(BehaviorContexts.IOnTickContext<THandle> context);
}