namespace CVMatrix.DropOffDefense.SLib.Util.RegionManagement;

using System.Numerics;
public interface ILineHandle<T> : IElementHandle<T> where T : class
{
    public IReadOnlyList<Vector2>? Path { get; }
}