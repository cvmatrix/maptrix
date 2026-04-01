namespace CVMatrix.DropOffDefense.SLib.Core.Internal;
using Handles;
using Behaviors;
using Actions;
using Messages;
using Stats;

internal class TravelNode : SimObject<ITravelNodeHandle, ITravelNodeBehavior, TravelNodeStats, ETravelNodeMessage, ETravelNodeAction>
{
    private Coordinates _position = null!;
    private HashSet<TravelWay> _blocked = [];
    public TravelNode(SimSystem sim, ITravelNodeBehavior behavior, TravelNodeStats stats) : base(sim, behavior, stats)
    {

    }

    public required Coordinates Position
    {
        get => _position;
        init => _position = value;
    }
    protected override void TickLogic(TimeSpan timestep)
    {
        throw new NotImplementedException();
    }

    public Handle GetHandle() => new(this);
    public class Handle(TravelNode source) : SimObjectHandle<TravelNode, ITravelNodeHandle, ITravelNodeBehavior, TravelNodeStats, ETravelNodeMessage, ETravelNodeAction>(source), ITravelNodeHandle
    {
        public Coordinates Position { get; }
        public IReadOnlySet<ITravelWayHandle> Blocked { get; }
        public IReadOnlySet<ITravelWayHandle> Incoming { get; }
        public IReadOnlySet<ITravelWayHandle> Outgoing { get; }
    }

    public override void RecieveActions(IEnumerable<ETravelNodeAction> actions)
    {
        throw new NotImplementedException();
    }
}
