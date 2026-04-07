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
    /// <summary>
    /// From -> To -> RoadType = Route.
    /// </summary>
    private Dictionary<TravelNode, Dictionary<TravelNode, Dictionary<ERoadType, IReadOnlyList<(TravelNode Node, double TotalCost)>?>>> _naiveOptimalRouteMap = [];
    internal List<TravelNode> Nodes { get; set; } = [];
    internal List<Traveler> Travelers { get; set; } = [];
    internal List<TravelWay> Ways { get; set; } = [];

    // type shit.
    internal IReadOnlyList<(TravelNode Node, double TotalCost)>? GetNaiveOptimalRoute(TravelNode from, TravelNode to, ERoadType roadType)
    {
        if (from.Equals(to)) return [];
        if (_naiveOptimalRouteMap.TryGetValue(from, out var f) &&
            f.TryGetValue(to, out var t) &&
            t.TryGetValue(roadType, out var route))
            return route;

        var matchingOutgoings = TravelNodeConnectionMap[from].Outgoing.Where(x => x.Stats.RoadType == roadType).ToArray();
        if (matchingOutgoings.Length == 0) return null;

        (TravelNode AddedNode, double TotalCost, IReadOnlyList<(TravelNode Node, double TotalCost)> ForwardRoute)? bestRoute = null;
        foreach (var outgoing in matchingOutgoings)
        {
            var nextNode = TravelWayConnectionMap[outgoing].To;
            var nextRoute = GetNaiveOptimalRoute(from, nextNode, roadType);
            if (nextRoute == null) continue;
            var thisCost = (nextRoute.Count > 0 ? nextRoute[0].TotalCost : 0) + outgoing.Distance / outgoing.NaiveAssumedSpeed;
            if (bestRoute is null || bestRoute.Value.TotalCost > thisCost)
                bestRoute = (nextNode, thisCost, nextRoute);
        }
        if (bestRoute == null)
        {
            _naiveOptimalRouteMap[from][to][roadType] = null;
            return null;
        }
        var br = bestRoute.Value;
        IReadOnlyList<(TravelNode Node, double TotalCost)> o = [(br.AddedNode, br.TotalCost), ..br.ForwardRoute];
        _naiveOptimalRouteMap[from][to][roadType] = o;
        return o;
    }
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