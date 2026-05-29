namespace CVMatrix.DropOffDefense.SLib.Util.ErgoLock;

using System.Threading;

public class UpgradeableErgoLockScope : IDisposable
{
    private readonly ReaderWriterLockSlim _parentLock;
    private bool _disposed;
    private bool _upgraded;

    public UpgradeableErgoLockScope(ReaderWriterLockSlim parentLock)
    {
        _parentLock = parentLock;
        _parentLock.EnterUpgradeableReadLock();
    }

    public void Dispose()
    {
        _parentLock.ExitWriteLock();
        _disposed = true;
    }

    public void UpgradeToWrite()
    {
        if (_disposed) throw new ObjectDisposedException("UpgradeableErgoLockScope");
        if (_upgraded) return;
        _parentLock.EnterWriteLock();
        _upgraded = true;
    }
}