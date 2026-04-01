namespace CVMatrix.DropOffDefense.SLib.Core;
using Handles;
public interface ISimContext
{
    public TimeSpan TimeStep { get; }
    public int TickIndex { get; }
    public IEnumerable<ITravelerHandle> AllTravelers { get; }
    public IEnumerable<ITravelNodeHandle> AllTravelNodes { get; }
    public IEnumerable<ITravelWayHandle> AllTravelWays { get; }
}