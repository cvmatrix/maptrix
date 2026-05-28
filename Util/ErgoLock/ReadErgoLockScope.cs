namespace CVMatrix.DropOffDefense.SLib.Util.ErgoLock;

using System.Threading;

public class ReadErgoLockScope : IDisposable
{
    private readonly ReaderWriterLockSlim _parentLock;

    public ReadErgoLockScope(ReaderWriterLockSlim parentLock)
    {
        _parentLock = parentLock;
        _parentLock.EnterReadLock();
    }

    public void Dispose()
    {
        _parentLock.ExitReadLock();
    }
}