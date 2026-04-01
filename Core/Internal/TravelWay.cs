namespace CVMatrix.DropOffDefense.SLib.Core.Internal;
using Handles;
using Behaviors;
using Actions;
using Messages;
using Stats;

internal class TravelWay : SimObject
{

    public TravelWay(SimSystem sim) : base(sim)
    {

    }
    public Handle GetHandle() => new(this);

    public class Handle(TravelWay source) : ITravelWayHandle
    {
        public TravelWay Source { get; } = source;
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

    protected override void TickLogic(TimeSpan timestep)
    {
        throw new NotImplementedException();
    }
}