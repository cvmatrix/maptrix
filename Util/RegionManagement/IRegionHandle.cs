namespace CVMatrix.DropOffDefense.SLib.Util.RegionManagement;

using System.Numerics;
public interface IRegionHandle<T> where T : class
{
    public IReadOnlySet<T> EncompassedBy { get; }
    public IReadOnlySet<T> Encompasses { get; }
    public IReadOnlyList<Vector2> Boundary { get; }
    public IReadOnlyList<IReadOnlyList<Vector2>> SubtractiveBoundaries { get; }
}