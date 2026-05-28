namespace CVMatrix.DropOffDefense.SLib.Util.ErgoLock;

using System.Threading;

public class WriteErgoLockScope : IDisposable
{
    private readonly ReaderWriterLockSlim _parentLock;

    public WriteErgoLockScope(ReaderWriterLockSlim parentLock)
    {
        _parentLock = parentLock;
        _parentLock.EnterWriteLock();
    }

    public void Dispose()
    {
        _parentLock.ExitWriteLock();
    }
}