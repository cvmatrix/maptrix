namespace CVMatrix.DropOffDefense.SLib.Core;

internal abstract class TickableObject : ITickable
{
    public ISimState? LastTickState { get; private set; }
    public void Tick(ISimState simState)
    {
        if (LastTickState == simState) return;
        var prevState = LastTickState;
        LastTickState = simState;
        TickInternal(simState, simState.TickIndex - (prevState?.TickIndex ?? -1));
    }
    protected abstract void TickInternal(ISimState simState, int times);
}