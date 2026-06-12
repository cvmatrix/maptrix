namespace CVMatrix.Maptrix.Util.ErgoLock;

public interface IUpgradeableScope : ILockScope
{
    public IWriteScope UpgradedScope { get; }
}