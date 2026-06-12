namespace CVMatrix.Maptrix.Internal;

using Trix;
using Trix.Tags;
using Util.RegionManagement;
using Util.Collections;
internal class Region : TaggableMapElement<IRegionTag>, ITrixRegion
{
    public required IReadOnlyList<Coordinates> Boundary { get; set; }
    public required IReadOnlyList<IReadOnlyList<Coordinates>> SubtractiveBoundaries { get; set; }
    IReadOnlyList<ITrixCoordinates> ITrixRegion.Boundary => Boundary;

    private IRegionHandle<Region, Way, IPointElement>? _regionHandle = null;

    private IRegionHandle<Region, Way, IPointElement> AccessRegionHandle()
    {
        _regionHandle ??= SourceMap.Data.RegionManager.GetRegion(this);
        return _regionHandle;
    }
    IReadOnlyList<IReadOnlyList<ITrixCoordinates>> ITrixRegion.SubtractiveBoundaries => SubtractiveBoundaries;

    IReadOnlySet<ITrixRegion> ITrixRegion.EncompassesRegions => AccessRegionHandle().EncompassesRegions.CastingView<Region, ITrixRegion>();
    IReadOnlySet<ITrixWay> ITrixRegion.EncompassesWays => AccessRegionHandle().EncompassesLines.CastingView<Way, ITrixWay>();
    IReadOnlySet<ITrixPoi> ITrixRegion.EncompassesPois => new HashSet<ITrixPoi>(AccessRegionHandle().EncompassesPoints.Where(x => x is Poi).Cast<ITrixPoi>());
    IReadOnlySet<ITrixIntersection> ITrixRegion.EncompassesIntersections => new HashSet<ITrixIntersection>(AccessRegionHandle().EncompassesPoints.Where(x => x is Intersection).Cast<ITrixIntersection>());

    protected override IElementHandle<Region> GetRegionElementHandle(RegionManager<Region, Way, IPointElement> manager)
    {
        return manager.GetRegion(this);
    }

    protected override IRegionTag? SerializeTag(string key, string value)
    {
        return null;
    }
}