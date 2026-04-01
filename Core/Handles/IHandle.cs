namespace CVMatrix.DropOffDefense.SLib.Core.Handles;

public interface IHandle<in TMessage>
{
    public void SendMessage(TMessage message);
}