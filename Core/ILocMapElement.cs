namespace CVMatrix.DropOffDefense.SLib.Core;

public interface ILocMapElement
{
    public IReadOnlyDictionary<string, string>? RawTags { get; }
    public LocMap Map { get; }
    public IReadOnlySet<ILocRegion> InRegions { get; }
}