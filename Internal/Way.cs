namespace CVMatrix.Maptrix.Internal;

using System.Numerics;
using Trix;
using Trix.Tags;
using Util.GraphManagement;
using Util.RegionManagement;

internal class Way : TaggableMapElement<IWayTag>, ITrixWay
{
    public float PathLength { get; private set; }
    public Way? AdjacentReverse { get; private set; }

    // DEV: it should be enforced that this edge always has From and To nodes before public exposure.
    public Intersection From => AccessGraphHandle().From!;
    public Intersection To => AccessGraphHandle().To!;

    public required IReadOnlyList<Coordinates> Path
    {
        get => _path;
        set
        {
            _path = value;
            PathLength = GetPathLength(_path);
        }
    }

    private IEdgeHandle<Intersection>? _graphHandle;

    private IReadOnlyList<Coordinates> _path = [];

    float ITrixWay.PathLength => PathLength;
    ITrixIntersection ITrixWay.From => From;
    ITrixIntersection ITrixWay.To => To;

    ITrixWay? ITrixWay.AdjacentReverse => AdjacentReverse;

    IReadOnlyList<ITrixCoordinates> ITrixWay.Path => Path;

    public Way? BreakFromAdjacentReverse()
    {
        var formerReverse = AdjacentReverse;
        if (formerReverse is null) return null;
        AdjacentReverse = null;
        formerReverse.AdjacentReverse = null;
        return formerReverse;
    }

    public Way CreateAdjacentReverse()
    {
        Way reverseWay = new()
        {
            Path = Path.Reverse().ToList(),
            SourceMap = SourceMap,
            RawTags = RawTags
        };
        SourceMap.Data.GraphManager.Connect(EConnectionDirection.From, To, reverseWay);
        SourceMap.Data.GraphManager.Connect(EConnectionDirection.To, From, reverseWay);
        if (AdjacentReverse is { } existingReverse)
            existingReverse.AdjacentReverse = null;
        AdjacentReverse = reverseWay;
        reverseWay.AdjacentReverse = this;
        SourceMap.Data.Ways.Add(reverseWay);
        SourceMap.Data.RegionManager.SetLine(reverseWay, reverseWay.Path.Select(x => x.Local.AsVector()));
        return reverseWay;
    }

    protected override IElementHandle<Region> GetRegionElementHandle(RegionManager<Region, Way, IPointElement> manager)
    {
        return manager.GetLine(this);
    }

    protected override IWayTag? SerializeTag(string key, string value)
    {
        return null;
    }

    private IEdgeHandle<Intersection> AccessGraphHandle()
    {
        _graphHandle ??= SourceMap.Data.GraphManager.GetEdge(this);
        return _graphHandle;
    }

    private static float GetPathLength(IEnumerable<Coordinates> path)
    {
        float o = 0;
        using var iter = path.GetEnumerator();
        if (!iter.MoveNext()) return o;
        var lastPoint = iter.Current.Local;
        while (iter.MoveNext())
        {
            var point = iter.Current.Local;
            o += Vector2.Distance(lastPoint, point);
            lastPoint = point;
        }

        return o;
    }
}