namespace CVMatrix.Maptrix.Trix;

public interface ITrixMapElement
{
    public IReadOnlyDictionary<string, string>? RawTags { get; }
    public IReadOnlySet<ITrixRegion> InRegions { get; }
    public TrixMap SourceMap { get; }
}