namespace CVMatrix.DropOffDefense.SLib.Util.RegionManagement;

using System.Numerics;
public interface IPointHandle<TRegion> : IElementHandle<TRegion> where TRegion : class
{
    public Vector2? Position { get; }
}