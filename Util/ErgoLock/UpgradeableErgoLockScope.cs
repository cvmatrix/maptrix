namespace CVMatrix.DropOffDefense.SLib.Util.ErgoLock;

using System.Threading;

public class UpgradeableErgoLockScope : IDisposable
{
    private readonly ReaderWriterLockSlim _parentLock;
    private bool _upgraded = false;
    private bool _disposed = false;
    public UpgradeableErgoLockScope(ReaderWriterLockSlim parentLock)
    {
        _parentLock = parentLock;
        _parentLock.EnterUpgradeableReadLock();
    }

    public void UpgradeToWrite()
    {
        if (_disposed) throw new ObjectDisposedException("UpgradeableErgoLockScope");
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