namespace CVMatrix.DropOffDefense.SLib.Internal;

using Loc;
using Loc.Tags;
using Util.Collections;
using Util.RegionManagement;

internal abstract class TaggableMapElement<TTagType> : ILocMapElement, ILocTaggable<TTagType> where TTagType : class, ITag
{
    private IElementHandle<Region>? _elementHandle = null;

    private IElementHandle<Region> AccessRegionElementHandle()
    {
        _elementHandle ??= GetRegionElementHandle(SourceMap.RegionManager);
        return _elementHandle;
    }
    protected abstract IElementHandle<Region> GetRegionElementHandle(RegionManager<Region, Way, IPointElement> manager);
    public required LocMap SourceMap { get; init; }
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

    IReadOnlySet<ILocRegion> ILocMapElement.InRegions => AccessRegionElementHandle().EncompassedBy.MappedView(ILocRegion (x) => x, x => (Region)x);

    public TTag? GetTag<TTag>() where TTag : class, TTagType
    {
        if (!_serialTagMap.TryGetValue(typeof(TTag), out var serialTag)) return null;
        return (TTag)serialTag;
    }

    protected abstract TTagType? SerializeTag(string key, string value);
}