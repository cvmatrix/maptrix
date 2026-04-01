namespace CVMatrix.DropOffDefense.SLib.Core;

using Handles;

public interface ISimEnvironment
{
    public IEnumerable<ITravelerHandle> AllTravelers { get; }
    public IEnumerable<ITravelNodeHandle> AllTravelNodes { get; }
    public IEnumerable<ITravelWayHandle> AllTravelWays { get; }
}