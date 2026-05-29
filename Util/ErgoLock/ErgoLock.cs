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

    public T InlineReadScope<T>(Func<T> func)
    {
        using var _ = ReadScope;
        return func();
    }
    public void InlineReadScope(Action action)
    {
        using var _ = ReadScope;
        action();
    }
    public T InlineWriteScope<T>(Func<T> func)
    {
        using var _ = WriteScope;
        return func();
    }
    public void InlineWriteScope(Action action)
    {
        using var _ = WriteScope;
        action();
    }
    public T InlineUpgradeableScope<T>(Func<UpgradeableErgoLockScope, T> func)
    {
        using var scope = UpgradeableScope;
        return func(scope);
    }
    public void InlineUpgradeableScope(Action<UpgradeableErgoLockScope> action)
    {
        using var scope = UpgradeableScope; 
        action(scope);
    }
}