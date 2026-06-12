namespace CVMatrix.Maptrix.Util.RegionManagement;

using System.Numerics;

public interface IElementHandle<TRegion> where TRegion : class
{
    public IReadOnlySet<TRegion> EncompassedBy { get; }
}