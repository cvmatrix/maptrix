namespace CVMatrix.DropOffDefense.SLib.Core;

using OverpassAPI.Model.Clean;
using Handles;
using Behaviors;
using Messages;
using Actions;
using Stats;

public class SimSystem
{
    private readonly int _tickIndex = -1;
    private readonly TimeSpan _currentTickTimestep = TimeSpan.Zero;
    private Dictionary<Traveler, Way> _travelerTravelingMap = [];
    private List<Node> _nodes = [];
    private List<Traveler> _travelers = [];
    private List<Way> _ways = [];

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

    private class Node
    {
    }

    private class Traveler(SimSystem sim, ITravelerBehavior behavior, TravelerStats stats)
    {
        private readonly ITravelerBehavior _behavior = behavior;
        private readonly SimSystem _sim = sim;
        private Handle? _cachedHandle = null;
        private int _lastTicked = -1;
        private TravelerStats _stats = stats;

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

        private class Handle(Traveler source) : ITravelerHandle
        {
            public Coordinates Position { get; }
            public double DistanceAlongWay { get; }
            public ITravelNodeHandle FinalTarget { get; }

            public ITravelWayHandle Traveling { get; }
            public Traveler Source { get; }
            public TravelerStats Stats { get; }

            public void EnsureUpdated()
            {
                Source.Tick();
            }

            public void SendMessage(ETravelerMessage message)
            {
                Source.RecieveActions(Source._behavior.OnRecieveMessage(message));
            }
        }

        private void RecieveActions(IEnumerable<ETravelerAction> actions)
        {
            throw new NotImplementedException();
        }
    }

    private class Way
    {
        public void AddTraveler(Traveler traveler)
        {
            throw new NotImplementedException();
        }
    }
}