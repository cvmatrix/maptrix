namespace CVMatrix.DropOffDefense.SLib.Core.Behaviors;

public interface IBehavior<in THandle, in TMessage, out TAction>
{
    public IEnumerable<TAction> OnRecieveMessage(TMessage message);
    public IEnumerable<TAction> OnTick(THandle self, ISimContext simContext, TimeSpan timestep);
}