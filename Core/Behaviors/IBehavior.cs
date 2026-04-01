namespace CVMatrix.DropOffDefense.SLib.Core.Behaviors;

public interface IBehavior<in THandle, in TMessage, out TAction>
{
    public IEnumerable<TAction> OnTick(THandle self, ISimState simState);
    public IEnumerable<TAction> OnRecieveMessage(TMessage message);
}