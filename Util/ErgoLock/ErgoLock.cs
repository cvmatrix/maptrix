namespace CVMatrix.DropOffDefense.SLib.Util.ErgoLock;

using System.Threading;
public class ErgoLock : IDisposable
{
    public ReaderWriterLockSlim InternalLock { get; } = new();
    public ReadErgoLockScope ReadScope => new(InternalLock);
    public WriteErgoLockScope WriteScope => new(InternalLock);
    public UpgradeableErgoLockScope UpgradeableScope => new(InternalLock);
    public void Dispose()
    {
        InternalLock.Dispose();
    }

    public T WithReadScope<T>(Func<T> func)
    {
        using var _ = ReadScope;
        return func();
    }
    public void WithReadScope(Action action)
    {
        using var _ = ReadScope;
        action();
    }
    public T WithWriteScope<T>(Func<T> func)
    {
        using var _ = WriteScope;
        return func();
    }
    public void WithWriteScope(Action action)
    {
        using var _ = WriteScope;
        action();
    }
    public T WithUpgradeableScope<T>(Func<UpgradeableErgoLockScope, T> func)
    {
        using var scope = UpgradeableScope;
        return func(scope);
    }
    public void WithUpgradeableScope(Action<UpgradeableErgoLockScope> action)
    {
        using var scope = UpgradeableScope;
        return action(scope);
    }
}