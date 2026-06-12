namespace CVMatrix.Maptrix.Internal;

using Trix;
using Trix.Tags;
using Util.GraphManagement;
using Util.Collections;
using Util.RegionManagement;

internal class Intersection : TaggableMapElement<IIntersectionTag>, ITrixIntersection, IPointElement
{
    public required Coordinates Position { get; set; }
    public IReadOnlySet<Way> Incoming => AccessGraphHandle().Incoming;
    public IReadOnlySet<Way> Outgoing => AccessGraphHandle().Outgoing;
    private INodeHandle<Way>? _graphHandle;
    ITrixCoordinates ITrixIntersection.Position => Position;

    // DEV: mapped view works because uhhhh it's garunteed that the only objects present are exactly the internal type probably uhh.

    IReadOnlySet<ITrixWay> ITrixIntersection.Incoming => Incoming.CastingView<Way, ITrixWay>();
    IReadOnlySet<ITrixWay> ITrixIntersection.Outgoing => Outgoing.CastingView<Way, ITrixWay>();

    protected override IElementHandle<Region> GetRegionElementHandle(RegionManager<Region, Way, IPointElement> manager)
    {
        return manager.GetPoint(this);
    }

    protected override IIntersectionTag? SerializeTag(string key, string value)
    {
        return null;
    }

    private INodeHandle<Way> AccessGraphHandle()
    {
        _graphHandle ??= SourceMap.Data.GraphManager.GetNode(this);
        return _graphHandle;
    }
}