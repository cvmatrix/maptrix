namespace CVMatrix.DropOffDefense.SLib.Core.Internal;
using Behaviors;
using Handles;
using Stats;
using Messages;
using Actions;

internal abstract class SimTickable(SimSystem sim)
{
    public int LastTicked { get; private set; } = -1;
    public SimSystem Sim { get; } = sim;
    public bool Tick()
    {
        var tickIndex = Sim.TickIndex;
        if (tickIndex <= LastTicked) return false;
        var ticks = tickIndex - LastTicked;
        var timestep = Sim.CurrentTickTimestep;
        LastTicked = tickIndex;

        for (var tick = 0; tick < ticks; tick++)
            TickLogic(timestep);
        return true;
    }

    protected abstract void TickLogic(TimeSpan timestep);
}