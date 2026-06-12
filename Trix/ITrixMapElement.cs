namespace CVMatrix.Maptrix.Trix;

public interface ILocMapElement
{
    public IReadOnlyDictionary<string, string>? RawTags { get; }
    public LocMap SourceMap { get; }
    public IReadOnlySet<ILocRegion> InRegions { get; }
}