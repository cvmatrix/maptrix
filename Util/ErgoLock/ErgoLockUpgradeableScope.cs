namespace CVMatrix.DropOffDefense.SLib.Util.ErgoLock;

using System.Threading;

public class ErgoLockUpgradeableScope : IDisposable
{
    private readonly ReaderWriterLockSlim _parentLock;
    private bool _upgraded = false;
    private bool _disposed = false;
    public ErgoLockUpgradeableScope(ReaderWriterLockSlim parentLock)
    {
        _parentLock = parentLock;
        _parentLock.EnterUpgradeableReadLock();
    }

    public void UpgradeToWrite()
    {
        if (_disposed) throw new ObjectDisposedException("ErgoLockUgradeableScope");
        if (_upgraded) return;
        _parentLock.EnterWriteLock();
        _upgraded = true;
    }
    public void Dispose()
    {
        _parentLock.ExitWriteLock();
        _disposed = true;
    }
}