namespace CVMatrix.DropOffDefense.SLib.Core;

using OverpassAPI.Model.Clean;
using Handles;
using Behaviors;
using Messages;
using Actions;
using Stats;
using Internal;
using System.Threading;
public class SimSystem : ISimEnvironment
{
    private readonly object _interactionLock = new();
    internal int TickIndex { get; private set; } = -1;
    internal TimeSpan CurrentTickTimestep { get; private set; }
    internal Dictionary<Traveler, TravelWay> TravelerTravelingMap { get; set; } = [];
    internal Dictionary<TravelWay, List<Traveler>> TravelWayTravelingMap { get; set; } = [];
    internal Dictionary<Traveler, int> TravelerPositionOnWayMap { get; set; } = [];
    internal Dictionary<TravelWay, (TravelNode From, TravelNode To)> TravelWayConnectionMap { get; set; } = [];
    internal Dictionary<TravelNode, (List<TravelWay> Incoming, List<TravelWay> Outgoing)> TravelNodeConnectionMap { get; set; } = [];
    internal Dictionary<TravelNode, Dictionary<ERoadType, List<TravelWay>>> NaiveOptimalRouteMap { get; set; } = [];
    internal List<TravelNode> Nodes { get; set; } = [];
    internal List<Traveler> Travelers { get; set; } = [];
    internal List<TravelWay> Ways { get; set; } = [];

    public void AddOverpassData(CleanData data)
    {
        lock (_interactionLock)
        {
            throw new NotImplementedException();
        }
    }

    public SimSystem Copy()
    {
        lock (_interactionLock)
        {
            throw new NotImplementedException();
        }
    }

    public TimeSpan Tick(TimeSpan timestep)
    {
        lock (_interactionLock)
        {
            throw new NotImplementedException();
        }
    }

    IEnumerable<ITravelerHandle> ISimEnvironment.AllTravelers => Travelers.Select(x => x.GetHandle());
    IEnumerable<ITravelNodeHandle> ISimEnvironment.AllTravelNodes => Nodes.Select(x => x.GetHandle());
    IEnumerable<ITravelWayHandle> ISimEnvironment.AllTravelWays => Ways.Select(x => x.GetHandle());
}