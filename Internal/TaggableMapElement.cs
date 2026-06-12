namespace CVMatrix.Maptrix.Internal;

using Trix;
using Trix.Tags;
using Util.Collections;
using Util.RegionManagement;

internal abstract class TaggableMapElement<TTagType> : ITrixMapElement, ITrixTaggable<TTagType> where TTagType : class, ITag
{
    public required TrixMap SourceMap { get; init; }

    public required IReadOnlyDictionary<string, string> RawTags
    {
        set
        {
            _rawTags = new(value);
            _serialTagMap = [];
            foreach (var (k, v) in _rawTags)
                if (SerializeTag(k, v) is { } serialTag)
                    _serialTagMap.Add(serialTag.GetType(), serialTag);
        }
        get => _rawTags;
    }

    private Dictionary<string, string> _rawTags = [];
    private Dictionary<Type, TTagType> _serialTagMap = [];
    private IElementHandle<Region>? _elementHandle;

    IReadOnlySet<ITrixRegion> ITrixMapElement.InRegions => AccessRegionElementHandle().EncompassedBy.CastingView<Region, ITrixRegion>();

    public TTag? GetTag<TTag>() where TTag : class, TTagType
    {
        if (!_serialTagMap.TryGetValue(typeof(TTag), out var serialTag)) return null;
        return (TTag)serialTag;
    }

    protected abstract IElementHandle<Region> GetRegionElementHandle(RegionManager<Region, Way, IPointElement> manager);

    protected abstract TTagType? SerializeTag(string key, string value);

    private IElementHandle<Region> AccessRegionElementHandle()
    {
        _elementHandle ??= GetRegionElementHandle(SourceMap.Data.RegionManager);
        return _elementHandle;
    }
}