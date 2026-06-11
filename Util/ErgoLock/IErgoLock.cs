namespace CVMatrix.DropOffDefense.SLib.Util.ErgoLock;

using System.Threading;

public interface IErgoLock : IDisposable
{
    public IReadScope ReadScope { get; }
    public IUpgradeableScope UpgradeableScope { get; }
    public IWriteScope WriteScope { get; }
}