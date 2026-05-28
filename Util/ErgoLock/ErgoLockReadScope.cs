namespace CVMatrix.DropOffDefense.SLib.Util.ErgoLock;

using System.Threading;

public class ErgoLockReadScope : IDisposable
{
    private readonly ReaderWriterLockSlim _parentLock;

    public ErgoLockReadScope(ReaderWriterLockSlim parentLock)
    {
        _parentLock = parentLock;
        _parentLock.EnterReadLock();
    }

    public void Dispose()
    {
        _parentLock.ExitReadLock();
    }
}