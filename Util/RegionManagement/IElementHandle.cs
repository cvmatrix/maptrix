namespace CVMatrix.DropOffDefense.SLib.Util.RegionManagement;

using System.Numerics;
public interface IElementHandle<T> where T : class
{
    public IReadOnlySet<T> EncompassedBy { get; }
    public IReadOnlySet<T> Encompasses { get; }
}