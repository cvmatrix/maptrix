namespace CVMatrix.DropOffDefense.SLib.Core.Handles;

public interface IHandle<in TMessage, out TStats>
{
    public TStats Stats { get; }
    public void SendMessage(TMessage message);
}