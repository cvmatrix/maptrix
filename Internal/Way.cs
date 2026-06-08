namespace CVMatrix.DropOffDefense.SLib.Internal;

using Loc;
using Loc.Tags;
using System.Numerics;
using Util.GraphManagement;
using Util.RegionManagement;

internal class Way : TaggableMapElement<IWayTag>, ILocWay
{
    public float PathLength { get; private set; }
    public required int WayConnectionId { get; set; }
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

    private IEdgeHandle<Intersection>? _graphHandle = null;

    private IReadOnlyList<Coordinates> _path = [];
    float ILocWay.PathLength => PathLength;
    ILocIntersection ILocWay.From => From;
    ILocIntersection ILocWay.To => To;

    ILocWay? ILocWay.AdjacentReverse => AdjacentReverse;
    int ILocWay.WayConnectionId => WayConnectionId;

    IReadOnlyList<ILocCoordinates> ILocWay.Path => Path;

    public Way? BreakFromAdjacentReverse()
    {
        var formerReverse = AdjacentReverse;
        if (formerReverse is null) return null;
        AdjacentReverse = null;
        formerReverse.AdjacentReverse = null;
        return formerReverse;
    }

    public Way CreateAdjacentReverse(int? connectionId)
    {
        Way reverseWay = new()
        {
            WayConnectionId = connectionId ?? WayConnectionId,
            Path = Path.Reverse().ToList(),
            SourceMap = SourceMap,
            RawTags = RawTags
        };
        SourceMap.GraphManager.Connect(EConnectionDirection.From, To, reverseWay);
        SourceMap.GraphManager.Connect(EConnectionDirection.To, From, reverseWay);
        if (AdjacentReverse is { } existingReverse)
            existingReverse.AdjacentReverse = null;
        AdjacentReverse = reverseWay;
        reverseWay.AdjacentReverse = this;
        return reverseWay;
    }

    protected override IElementHandle<Region> GetRegionElementHandle(RegionManager<Region, Way, IPointElement> manager) => manager.GetLine(this);

    protected override IWayTag? SerializeTag(string key, string value)
    {
        throw new NotImplementedException();
    }

    private IEdgeHandle<Intersection> AccessGraphHandle()
    {
        _graphHandle ??= SourceMap.GraphManager.GetEdge(this);
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