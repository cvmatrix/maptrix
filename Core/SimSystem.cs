namespace CVMatrix.DropOffDefense.SLib.Core;

using OverpassAPI.Model.Clean;
using Handles;
using Behaviors;
using Messages;
using Actions;
using Stats;

public class SimSystem
{
    private int _tickIndex = -1;
    private List<Traveler> _travelers = [];
    private List<Node> _nodes = [];
    private List<Way> _ways = [];
    private Dictionary<Traveler, Way> _travelerTravelingMap = [];
    public void AddOverpassData(CleanData data)
    {
        throw new NotImplementedException();
    }
    public SimSystem Copy()
    {
        throw new NotImplementedException();
    }
    public TimeSpan Tick(TimeSpan timestep)
    {
        throw new NotImplementedException();
    }

    private class Traveler(SimSystem sim, ITravelerBehavior behavior, TravelerStats stats)
    {
        private readonly SimSystem _sim = sim;
        private int _lastTicked = -1;
        private ITravelerBehavior _behavior = behavior;
        private TravelerStats _stats = stats;
        private Handle? _cachedHandle = null;
        public ITravelerHandle Tick(int tickIndex, TimeSpan timestep)
        {
            if (tickIndex <= _lastTicked) return _cachedHandle!;
            var ticks = tickIndex - _lastTicked;
            _lastTicked = tickIndex;

        }

        private class Handle : ITravelerHandle
        {
            public TravelerStats Stats { get; }
            public void SendMessage(ETravelerMessage message)
            {
                throw new NotImplementedException();
            }
            public ITravelWayHandle Traveling { get; }
            public Coordinates Position { get; }
            public double DistanceAlongWay { get; }
            public ITravelNodeHandle FinalTarget { get; }
        }
    }

    private class Node
    {

    }

    private class Way
    {
        public void AddTraveler(Traveler traveler)
        {
            throw new NotImplementedException();
        }
    }
}