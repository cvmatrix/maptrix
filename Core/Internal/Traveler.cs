namespace CVMatrix.DropOffDefense.SLib.Core.Internal;
using Behaviors;
using Handles;
using Stats;
using Messages;
using Actions;
internal class Traveler : SimObject<ITravelerHandle, ITravelerBehavior, TravelerStats, ETravelerMessage, ETravelerAction>
{
    public Traveler(SimSystem sim, ITravelerBehavior behavior, TravelerStats stats, TravelNode target) : base(sim, behavior, stats)
    {
        Speed = 0;
        _distanceAlongWay = 0;
        FinalTarget = target;
    }
    public double Speed { get; private set; }
    private double _distanceAlongWay;

    public double DistanceAlongWay
    {
        get => _distanceAlongWay;
        private set
        {

            bool forward = value >= _distanceAlongWay;
            _distanceAlongWay = value;
            var travelers = Sim.TravelWayTravelingMap[Sim.TravelerTravelingMap[this]];
            if (forward)
            {
                for (int i = Sim.TravelerIndexOnWayMap[this]; i + 1 < travelers.Count && travelers[i + 1].DistanceAlongWay < DistanceAlongWay; i++)
                {
                    Sim.TravelerIndexOnWayMap[this] = i + 1;
                    Sim.TravelerIndexOnWayMap[travelers[i + 1]] = i;
                    (travelers[i], travelers[i + 1]) = (travelers[i + 1], travelers[i]);
                }
            }
            else
            {
                for (int i = Sim.TravelerIndexOnWayMap[this]; i - 1 >= 0 && travelers[i - 1].DistanceAlongWay > DistanceAlongWay; i--)
                {
                    Sim.TravelerIndexOnWayMap[this] = i - 1;
                    Sim.TravelerIndexOnWayMap[travelers[i - 1]] = i;
                    (travelers[i], travelers[i - 1]) = (travelers[i - 1], travelers[i]);
                }
            }
        }
    }

    public TravelNode FinalTarget { get; private set; }

    protected override void TickLogic(TimeSpan timestep)
    {
        Speed = double.Clamp(Speed + Stats.Acceleration * timestep.TotalSeconds, 0, Stats.MaxSpeed);
        throw new NotImplementedException();
    }

    private (double Distance, double Speed)? GetClosestBlockage()
    {
        var way = Sim.TravelerTravelingMap[this];
        var travelers = Sim.TravelWayTravelingMap[way];
        var lanes = way.Stats.LaneCount;
        var blocking = new Queue<Traveler>();

        for (var i = Sim.TravelerIndexOnWayMap[this] + 1; i + lanes < travelers.Count; i++)
        {
            var other = travelers[i];
            if (other.DistanceAlongWay > DistanceAlongWay + Stats.SightDistance) break;
            var backReach = other.DistanceAlongWay - other.Stats.Size;
            while (blocking.TryPeek(out var first) && first.DistanceAlongWay < backReach)
                blocking.Dequeue();
            blocking.Enqueue(other);
            if (blocking.Count < lanes) continue;
            var minDistance = double.MaxValue;
            var maxSpeed = double.MinValue;
            foreach (var blocker in blocking)
            {
                var blockerReach = blocker.DistanceAlongWay + blocker.Stats.Size;
                if (!(blockerReach < minDistance)) continue;
                minDistance = blockerReach;
                maxSpeed = blocker.Speed;
            }
            return (minDistance, maxSpeed);

        }
        return null;
    }
    protected override ITravelerHandle GenerateHandle() => new Handle(this);

    public class Handle(Traveler source) : SimObjectHandle<Traveler, ITravelerHandle, ITravelerBehavior, TravelerStats, ETravelerMessage, ETravelerAction>(source), ITravelerHandle
    {
        public double DistanceAlongWay => Source.DistanceAlongWay;
        public ITravelNodeHandle FinalTarget => Source.FinalTarget.GetHandle();
        public ITravelWayHandle Traveling => Source.Sim.TravelerTravelingMap[Source].GetHandle();
    }

    public override void RecieveActions(IEnumerable<ETravelerAction> actions)
    {
        foreach (var action in actions)
            switch (action)
            {
                default:
                    throw new NotSupportedException();
            }
    }
}
