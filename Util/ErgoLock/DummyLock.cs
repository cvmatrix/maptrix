namespace CVMatrix.Maptrix.Util.ErgoLock;

using System.Threading;

public class DummyLock : IErgoLock
{
    public IReadScope ReadScope => _scopeInstance;
    public IUpgradeableScope UpgradeableScope => _scopeInstance;
    public IWriteScope WriteScope => _scopeInstance;
    private static readonly DummyScope _scopeInstance = new();

    public void Dispose()
    {
    }

    private class DummyScope : IReadScope, IWriteScope, IUpgradeableScope
    {
        public IWriteScope UpgradedScope => this;

        public void Dispose()
        {
        }
    }
}