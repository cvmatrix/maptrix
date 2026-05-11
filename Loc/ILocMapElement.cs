namespace CVMatrix.DropOffDefense.SLib.Loc;

public interface ILocMapElement
{
    public IReadOnlyDictionary<string, string>? RawTags { get; }
    public LocMap Map { get; }
    public IReadOnlySet<ILocRegion> InRegions { get; }
}