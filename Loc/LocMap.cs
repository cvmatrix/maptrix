namespace CVMatrix.DropOffDefense.SLib.Loc;

using OverpassAPI;
using OverpassAPI.Model;
using OverpassAPI.Model.Clean;
using Util.GraphManagement;
using Util.Collections;
using Util.RegionManagement;
using Internal;
using GraphManager = Util.GraphManagement.GraphManager<Internal.Intersection, Internal.Way>;
using RegionManager = Util.RegionManagement.RegionManager<Internal.Region, Internal.Way, Internal.IPointElement>;

public class LocMap
{

    internal Resources Data;
    public ProjectionSource Projection => Data.Projection;
    public IReadOnlySet<ILocRegion> Regions => Data.Regions.CastingView<Region, ILocRegion>();
    public IReadOnlySet<ILocIntersection> Intersections => Data.Intersections.CastingView<Intersection, ILocIntersection>();
    public IReadOnlySet<ILocWay> Ways => Data.Ways.CastingView<Way, ILocWay>();
    public IReadOnlySet<ILocPoi> Pois => Data.Pois.CastingView<Poi, ILocPoi>();

    private LocMap(Resources data)
    {
        Data = data;
    }

    public static LocMap FromOverpassData(CleanData data, ProjectionSource projection)
    {
        var locData = new Resources()
        {
            Projection = projection,
            GraphManager = new(),
            RegionManager = new(),
            Regions = [],
            Intersections = [],
            Ways = [],
            Pois = [],
        };

        throw new NotImplementedException();
    }

    internal class Resources
    {
        internal required ProjectionSource Projection { get; set; }
        internal required GraphManager GraphManager { get; set; }
        internal required RegionManager RegionManager { get; set; }
        internal required HashSet<Region> Regions { get; set; }
        internal required HashSet<Intersection> Intersections { get; set; }
        internal required HashSet<Way> Ways { get; set; }
        internal required HashSet<Poi> Pois { get; set; }
    }
}