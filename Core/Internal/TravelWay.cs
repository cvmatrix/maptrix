namespace CVMatrix.DropOffDefense.SLib.Core.Internal;
using Handles;
using Behaviors;
using Actions;
using Messages;
using Stats;
internal class TravelWay : SimObject<ITravelWayHandle, ITravelWayBehavior, TravelWayStats, ETravelWayMessage, ETravelWayAction>
{

    public TravelWay(SimSystem sim, ITravelWayBehavior behavior, TravelWayStats stats) : base(sim, behavior, stats)
    {

    }

    public double Distance { get; private set; }
    public double NaiveAssumedSpeed => Stats.SpeedLimit;
    public class Handle(TravelWay source) : ITravelWayHandle
    {
        public TravelWay Source { get; } = source;
        // DEV: remember to recalculate optimal route when changing speed limit.
        public TravelWayStats Stats { get; }
        public void EnsureUpdated()
        {
            throw new NotImplementedException();
        }

        public void SendMessage(ETravelWayMessage message)
        {
            throw new NotImplementedException();
        }

        public double Distance { get; }
        public IReadOnlyList<Coordinates> Path { get; }
        public IReadOnlyList<ITravelerHandle> Travelers { get; }
        public ITravelNodeHandle From { get; }
        public ITravelNodeHandle To { get; }
        public bool IsImpeded { get; }
    }

    protected override ITravelWayHandle GenerateHandle()
    {
        throw new NotImplementedException();
    }

    public override void RecieveActions(IEnumerable<ETravelWayAction> actions)
    {
        throw new NotImplementedException();
    }

    protected override void TickLogic(TimeSpan timestep)
    {
        throw new NotImplementedException();
    }
}