namespace CVMatrix.DropOffDefense.SLib.Util.RegionManagement;

using System.Numerics;

public interface IRegionHandle<TRegion, TLine, TPoint> : IElementHandle<TRegion> where TRegion : class where TLine : class where TPoint : class
{
    public IReadOnlyList<IReadOnlyList<Vector2>>? SubtractiveBoundaries { get; }
    public IReadOnlyList<Vector2>? Boundary { get; }
    public IReadOnlySet<TLine> EncompassesLines { get; }
    public IReadOnlySet<TPoint> EncompassesPoints { get; }
    public IReadOnlySet<TRegion> EncompassesRegions { get; }
}