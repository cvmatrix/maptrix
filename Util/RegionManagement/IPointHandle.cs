namespace CVMatrix.DropOffDefense.SLib.Util.RegionManagement;

using System.Numerics;
public interface IPointHandle<T> : IElementHandle<T> where T : class
{
    public Vector2? Position { get; }
}