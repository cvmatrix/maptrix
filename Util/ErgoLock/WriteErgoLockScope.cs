namespace CVMatrix.DropOffDefense.SLib.Util.ErgoLock;

using System.Threading;

public class ErgoLockWriteScope : IDisposable
{
    private readonly ReaderWriterLockSlim _parentLock;

    public ErgoLockWriteScope(ReaderWriterLockSlim parentLock)
    {
        _parentLock = parentLock;
        _parentLock.EnterWriteLock();
    }

    public void Dispose()
    {
        _parentLock.ExitWriteLock();
    }
}