namespace CVMatrix.DropOffDefense.SLib.Loc;

public interface ILocMapElement
{
    public IReadOnlyDictionary<string, string>? RawTags { get; }
    public LocMap SourceMap { get; }
    public IReadOnlySet<ILocRegion> InRegions { get; }
}