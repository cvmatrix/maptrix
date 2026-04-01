namespace CVMatrix.DropOffDefense.SLib.Core.Internal;
using Behaviors;
using Handles;
using Stats;
using Messages;
using Actions;
internal class Traveler : SimTickable
{
    public Traveler(SimSystem sim, ITravelerBehavior behavior, TravelerStats stats, TravelNode target) : base(sim)
    {
        Behavior = behavior;
        Stats = stats;
        Speed = 0;
        LastTicked = -1;
        DistanceAlongWay = 0;
        FinalTarget = target;
        _cachedPosition = null;
    }
    public ITravelerBehavior Behavior { get; }
    public double Speed { get; private set; }
    public int LastTicked { get; private set; }
    public TravelerStats Stats { get; private set; }
    public double DistanceAlongWay { get; private set; }
    public TravelNode FinalTarget { get; private set; }

    protected override void TickLogic(TimeSpan timestep)
    {
        throw new NotImplementedException();
    }

    public Handle GetHandle() => new(this);
    public class Handle : ITravelerHandle
    {
        public double DistanceAlongWay => Source.DistanceAlongWay;
        public ITravelNodeHandle FinalTarget => Source.FinalTarget.GetHandle();
        public ITravelWayHandle Traveling => Source.Sim.TravelerTravelingMap[Source].GetHandle();
        public Traveler Source { get; }
        public TravelerStats Stats => Source.Stats;

        public Handle(Traveler source)
        {
            Source = source;
        }
        public void EnsureUpdated()
        {
            Source.Tick();
        }
        public void SendMessage(ETravelerMessage message)
        {
            Source.RecieveActions(Source.Behavior.OnRecieveMessage(message));
        }
        public override bool Equals(object? obj) => obj is Handle other && Source.Equals(other.Source);
        public override int GetHashCode() => Source.GetHashCode();
    }

    private void RecieveActions(IEnumerable<ETravelerAction> actions)
    {
        throw new NotImplementedException();
    }
}