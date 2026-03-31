namespace CVMatrix.DropOffDefense.SLib.Core;

public interface ISimState
{
    public TimeSpan TimeStep { get; }
    public int TickIndex { get; }
}