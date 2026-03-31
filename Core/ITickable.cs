namespace CVMatrix.DropOffDefense.SLib.Core;

public interface ITickable
{
    /// <summary>
    /// Lower is higher priority.
    /// </summary>
    public int TickPriority { get; }
    public void Tick(TimeSpan timestep);
}