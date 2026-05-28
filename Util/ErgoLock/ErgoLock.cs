namespace CVMatrix.DropOffDefense.SLib.Util.ErgoLock;

using System.Threading;
public class ErgoLock : IDisposable
{
    public ReaderWriterLockSlim InternalLock { get; } = new();
    public ErgoLockReadScope ReadScope => new(InternalLock);
    public ErgoLockWriteScope WriteScope => new(InternalLock);
    public ErgoLockWriteScope UpgradeableScope => new(InternalLock);
    public void Dispose()
    {
        InternalLock.Dispose();
    }
}