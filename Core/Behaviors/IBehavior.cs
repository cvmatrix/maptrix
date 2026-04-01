namespace CVMatrix.DropOffDefense.SLib.Core.Behaviors;

public interface IBehavior<in THandle, in TMessage>
{
    public void OnTick(THandle self, ISimState simState);
    public void OnRecieveMessage(TMessage message);
}