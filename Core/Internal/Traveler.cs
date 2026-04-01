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
        DistanceAlongWay = 0;
        FinalTarget = target;
    }
    public double Speed { get; private set; }
    public double DistanceAlongWay { get; private set; }
    public TravelNode FinalTarget { get; private set; }

    protected override void TickLogic(TimeSpan timestep)
    {
        throw new NotImplementedException();
    }

    public Handle GetHandle() => new(this);
    public class Handle(Traveler source) : SimObjectHandle<Traveler, ITravelerHandle, ITravelerBehavior, TravelerStats, ETravelerMessage, ETravelerAction>(source), ITravelerHandle
    {
        public double DistanceAlongWay => Source.DistanceAlongWay;
        public ITravelNodeHandle FinalTarget => Source.FinalTarget.GetHandle();
        public ITravelWayHandle Traveling => Source.Sim.TravelerTravelingMap[Source].GetHandle();
    }

    public override void RecieveActions(IEnumerable<ETravelerAction> actions)
    {
        throw new NotImplementedException();
    }
}
