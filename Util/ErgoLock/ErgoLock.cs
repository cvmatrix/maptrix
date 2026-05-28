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
}