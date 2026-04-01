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
    private TimeSpan _currentTickTimestep = TimeSpan.Zero;
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
        public bool Tick()
        {
            var tickIndex = _sim._tickIndex;
            if (tickIndex <= _lastTicked) return false;
            var ticks = tickIndex - _lastTicked;
            var timestep = _sim._currentTickTimestep;
            _lastTicked = tickIndex;

            throw new NotImplementedException();

            return true;
        }
        private void RecieveActions(IEnumerable<ETravelerAction> actions)
        {
            throw new NotImplementedException();
        }
        private class Handle(Traveler source) : ITravelerHandle
        {
            public Traveler Source { get; }
            public TravelerStats Stats { get; }
            public void SendMessage(ETravelerMessage message)
            {
                Source.RecieveActions(Source._behavior.OnRecieveMessage(message));
            }

            public void EnsureUpdated() => Source.Tick();
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