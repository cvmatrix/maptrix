namespace CVMatrix.DropOffDefense.SLib.Core.Internal;
using Handles;
using Behaviors;
using Actions;
using Messages;
using Stats;

internal class TravelNode : SimTickable
{

    public TravelNode(SimSystem sim) : base(sim)
    {

    }
    public Handle GetHandle() => new(this);
    public class Handle(TravelNode source) : ITravelNodeHandle
    {
        public Coordinates Position { get; }
        public TravelNode Source { get; } = source;
        public TravelNodeStats Stats { get; }
        public void EnsureUpdated()
        {
            throw new NotImplementedException();
        }

        public void SendMessage(ETravelNodeMessage message)
        {
            throw new NotImplementedException();
        }

        public IReadOnlySet<ITravelWayHandle> Blocked { get; }
        public IReadOnlySet<ITravelWayHandle> Incoming { get; }
        public IReadOnlySet<ITravelWayHandle> Outgoing { get; }
    }

    protected override void TickLogic(TimeSpan timestep)
    {
        throw new NotImplementedException();
    }
}