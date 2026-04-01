namespace CVMatrix.DropOffDefense.SLib.Core;

// DEV:
// any objects referenced by object X should be ticked by object X before using.
// reference loops cause the last unique object to see the first object as unticked.
internal abstract class TickableObject : ITickable
{
    public ISimState? LastTickState { get; private set; }
    public void Tick(ISimState simState)
    {
        if (LastTickState == simState) return;
        var prevState = LastTickState;
        LastTickState = simState;
        for (int i = 0; i < simState.TickIndex - (prevState?.TickIndex ?? -1); i++)
            TickInternal(simState);
    }
    protected abstract void TickInternal(ISimState simState);
}