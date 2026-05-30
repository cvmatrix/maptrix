namespace CVMatrix.DropOffDefense.SLib.Internal;

using Loc;
using Loc.Tags;

internal abstract class TaggableMapElement<TTagType> : ILocMapElement, ILocTaggable<TTagType> where TTagType : class, ITag
{
    public HashSet<ILocRegion> InRegions { get; } = [];
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

    IReadOnlySet<ILocRegion> ILocMapElement.InRegions => InRegions;

    public TTag? GetTag<TTag>() where TTag : class, TTagType
    {
        if (!_serialTagMap.TryGetValue(typeof(TTag), out var serialTag)) return null;
        return (TTag)serialTag;
    }

    protected abstract TTagType? SerializeTag(string key, string value);
}