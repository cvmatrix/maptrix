namespace CVMatrix.DropOffDefense.SLib.Loc;

using OverpassAPI;
using OverpassAPI.Model;
using OverpassAPI.Model.Clean;

public class LocMap
{
    public ProjectionSource Projection { get; }
    public IReadOnlySet<ILocRegion> Regions { get; }
    public IReadOnlySet<ILocIntersection> Intersections { get; }
    public IReadOnlySet<ILocWay> Ways { get; }
    public IReadOnlySet<ILocPoi> Pois { get; }

    private LocMap()
    {

    }

    public static LocMap FromOverpassData(CleanData data, ProjectionSource projection)
    {
        throw new NotImplementedException();
    }

    
}