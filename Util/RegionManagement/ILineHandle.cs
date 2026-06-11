namespace CVMatrix.DropOffDefense.SLib.Util.RegionManagement;

using System.Numerics;

public interface ILineHandle<TRegion> : IElementHandle<TRegion> where TRegion : class
{
    public IReadOnlyList<Vector2>? Path { get; }
}