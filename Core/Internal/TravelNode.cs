namespace CVMatrix.DropOffDefense.SLib.Core.Internal;
using Handles;
using Behaviors;
using Actions;
using Messages;
using Stats;
using Util;
internal class TravelNode : SimObject<ITravelNodeHandle, ITravelNodeBehavior, TravelNodeStats, ETravelNodeMessage, ETravelNodeAction>
{
    public TravelNode(SimSystem sim, ITravelNodeBehavior behavior, TravelNodeStats stats) : base(sim, behavior, stats)
    {
        
    }

    public required Coordinates Position { get; set; }
    public HashSet<TravelWay> Blocked { get; set; } = [];
    protected override void TickLogic(TimeSpan timestep)
    {
        throw new NotImplementedException();
    }

    protected override ITravelNodeHandle GenerateHandle() => new Handle(this);
    public class Handle(TravelNode source) : SimObjectHandle<TravelNode, ITravelNodeHandle, ITravelNodeBehavior, TravelNodeStats, ETravelNodeMessage, ETravelNodeAction>(source), ITravelNodeHandle
    {
        public Coordinates Position => Source.Position;
        public IReadOnlySet<ITravelWayHandle> Blocked { get; }
        public IReadOnlySet<ITravelWayHandle> Incoming { get; }
        public IReadOnlySet<ITravelWayHandle> Outgoing { get; }
    }

    public override void RecieveActions(IEnumerable<ETravelNodeAction> actions)
    {
        throw new NotImplementedException();
    }
}
