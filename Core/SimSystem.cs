namespace CVMatrix.DropOffDefense.SLib.Core;

public class SimSystem
{
    public SimSystem Copy()
    {
        throw new NotImplementedException();
    }
    public TimeSpan Tick(TimeSpan timestep)
    {
        throw new NotImplementedException();
    }
    private IEnumerable<ITickable> GetTickOrder()
    {
        throw new NotImplementedException();
    }
}