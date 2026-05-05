namespace CVMatrix.DropOffDefense.SLib.Core;

public interface ILocMapElement
{
    public LocMap Map { get; }
    public IReadOnlySet<ILocRegion> InRegions { get; }
}