namespace CVMatrix.DropOffDefense.SLib.Util.ErgoLock;

public interface IUpgradeableScope : ILockScope
{
    public IWriteScope UpgradedScope { get; }
}