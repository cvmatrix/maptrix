namespace CVMatrix.DropOffDefense.SLib.Util.ErgoLock;

using System.Threading;

public class ErgoLock : IErgoLock
{
    public ReaderWriterLockSlim InternalLock { get; }

    public IReadScope ReadScope => new ReadScopeHolder(this);
    public IUpgradeableScope UpgradeableScope => new UpgradeableScopeHolder(this);
    public IWriteScope WriteScope => new WriteScopeHolder(this);

    public ErgoLock()
    {
        InternalLock = new();
    }

    public ErgoLock(ReaderWriterLockSlim internalLock)
    {
        InternalLock = internalLock;
    }

    public void Dispose()
    {
        InternalLock.Dispose();
    }

    private class ReadScopeHolder : IReadScope
    {
        private readonly ErgoLock _parent;

        public ReadScopeHolder(ErgoLock parent)
        {
            _parent = parent;
            _parent.InternalLock.EnterReadLock();
        }

        public void Dispose()
        {
            _parent.InternalLock.ExitReadLock();
        }
    }

    private class UpgradeableScopeHolder : IUpgradeableScope
    {
        public IWriteScope UpgradedScope => !_disposed ? new WriteScopeHolder(_parent) : throw new ObjectDisposedException("IUpgradeableScope");
        private readonly ErgoLock _parent;
        private bool _disposed;

        public UpgradeableScopeHolder(ErgoLock parent)
        {
            _parent = parent;
            _parent.InternalLock.EnterUpgradeableReadLock();
        }

        public void Dispose()
        {
            _disposed = true;
            _parent.InternalLock.ExitUpgradeableReadLock();
        }
    }

    private class WriteScopeHolder : IWriteScope
    {
        private readonly ErgoLock _parent;

        public WriteScopeHolder(ErgoLock parent)
        {
            _parent = parent;
            _parent.InternalLock.EnterWriteLock();
        }

        public void Dispose()
        {
            _parent.InternalLock.ExitWriteLock();
        }
    }
}