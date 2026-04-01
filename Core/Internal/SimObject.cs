namespace CVMatrix.DropOffDefense.SLib.Core.Internal;
using Behaviors;
using Handles;
using Stats;
using Messages;
using Actions;

internal abstract class SimObject<THandle, TBehavior, TStats, TMessage, TAction>(SimSystem sim, TBehavior behavior, TStats stats) where TBehavior : IBehavior<THandle, TMessage, TAction>
{
    public int LastTicked { get; private set; } = -1;
    public SimSystem Sim { get; } = sim;
    public TBehavior Behavior { get; } = behavior;
    public TStats Stats { get; } = stats;
    // bro.
    private bool _handleCached = false;
    private THandle? _cachedHandle = default;

    public bool Tick()
    {
        var tickIndex = Sim.TickIndex;
        if (tickIndex <= LastTicked) return false;
        var ticks = tickIndex - LastTicked;
        var timestep = Sim.CurrentTickTimestep;
        LastTicked = tickIndex;
        _handleCached = false;
        _cachedHandle = default;
        for (var tick = 0; tick < ticks; tick++)
            TickLogic(timestep);
        return true;
    }

    public THandle GetHandle()
    {
        if (!_handleCached)
        {
            _handleCached = true;
            _cachedHandle = GenerateHandle();
        }
        return _cachedHandle!;
    }
    protected abstract THandle GenerateHandle();
    public abstract void RecieveActions(IEnumerable<TAction> actions);
    protected abstract void TickLogic(TimeSpan timestep);
}