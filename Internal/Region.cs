namespace CVMatrix.Maptrix.Internal;

using Loc;
using Loc.Tags;
using Util.RegionManagement;
using Util.Collections;
internal class Region : TaggableMapElement<IRegionTag>, ILocRegion
{
    public required IReadOnlyList<Coordinates> Boundary { get; set; }
    public required IReadOnlyList<IReadOnlyList<Coordinates>> SubtractiveBoundaries { get; set; }
    IReadOnlyList<ILocCoordinates> ILocRegion.Boundary => Boundary;

    private IRegionHandle<Region, Way, IPointElement>? _regionHandle = null;

    private IRegionHandle<Region, Way, IPointElement> AccessRegionHandle()
    {
        _regionHandle ??= SourceMap.Data.RegionManager.GetRegion(this);
        return _regionHandle;
    }
    IReadOnlyList<IReadOnlyList<ILocCoordinates>> ILocRegion.SubtractiveBoundaries => SubtractiveBoundaries;

    IReadOnlySet<ILocRegion> ILocRegion.EncompassesRegions => AccessRegionHandle().EncompassesRegions.CastingView<Region, ILocRegion>();
    IReadOnlySet<ILocWay> ILocRegion.EncompassesWays => AccessRegionHandle().EncompassesLines.CastingView<Way, ILocWay>();
    IReadOnlySet<ILocPoi> ILocRegion.EncompassesPois => new HashSet<ILocPoi>(AccessRegionHandle().EncompassesPoints.Where(x => x is Poi).Cast<ILocPoi>());
    IReadOnlySet<ILocIntersection> ILocRegion.EncompassesIntersections => new HashSet<ILocIntersection>(AccessRegionHandle().EncompassesPoints.Where(x => x is Intersection).Cast<ILocIntersection>());

    protected override IElementHandle<Region> GetRegionElementHandle(RegionManager<Region, Way, IPointElement> manager)
    {
        return manager.GetRegion(this);
    }

    protected override IRegionTag? SerializeTag(string key, string value)
    {
        return null;
    }
}