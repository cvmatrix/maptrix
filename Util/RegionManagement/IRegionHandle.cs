namespace CVMatrix.DropOffDefense.SLib.Util.RegionManagement;

using System.Numerics;
public interface IRegionHandle<T> : IElementHandle<T> where T : class
{
    public IReadOnlyList<Vector2>? Boundary { get; }
    public IReadOnlyList<IReadOnlyList<Vector2>>? SubtractiveBoundaries { get; }
}