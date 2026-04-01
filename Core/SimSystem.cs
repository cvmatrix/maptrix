namespace CVMatrix.DropOffDefense.SLib.Core;

using OverpassAPI.Model.Clean;
using Handles;
using Behaviors;
using Messages;
using Actions;
using Stats;
using Internal;
public class SimSystem
{
    internal int TickIndex { get; private set; } = -1;
    internal TimeSpan CurrentTickTimestep { get; private set; }
    internal Dictionary<Traveler, TravelWay> TravelerTravelingMap { get; set; } = [];
    internal List<TravelNode> Nodes { get; set; } = [];
    internal List<Traveler> Travelers { get; set; } = [];
    internal List<TravelWay> Ways { get; set; } = [];

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
}