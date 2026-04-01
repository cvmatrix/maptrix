namespace CVMatrix.DropOffDefense.SLib.Core.BehaviorContexts;

public interface IBehaviorContext<out THandle>
{
    public ISimEnvironment Environment { get; }
    public THandle Self { get; }
}