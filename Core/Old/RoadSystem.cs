namespace CVMatrix.DropOffDefense.SLib.Core.Old;

using OverpassAPI;
using OverpassAPI.Model.Clean;

public class RoadSystem
{
    private const double EARTH_RADIUS = 6_371_000;
    // ts is going to be crazy
    public static RoadSystem FromOverpassData(CleanData data, RawCoordinates origin)
    {
        var rawHighways = new List<CleanWay>();
        foreach (var way in data.Ways.Values)
        {
            if (way.Tags.ContainsKey("highway")) rawHighways.Add(way);
        }

        var inverseNodeMap = GetInverseNodeMap(rawHighways);

        
        throw new NotImplementedException();
    }

    private static Dictionary<ulong, List<CleanWay>> GetInverseNodeMap(IEnumerable<CleanWay> ways)
    {
        var o = new Dictionary<ulong, List<CleanWay>>();
        foreach (var highway in ways)
        {
            foreach (var nodeId in highway.Nodes)
            {
                if (o.TryGetValue(nodeId, out var intersecting))
                {
                    intersecting.Add(highway);
                }
                else
                {
                    o.Add(nodeId, [highway]);
                }
            }
        }
        return o;
    }

    private static List<CleanWay> MergeWayEnds(IReadOnlyCollection<CleanWay> ways, Func<CleanWay, CleanWay, bool> additionalMergePredicate)
    {
        var inverseNodeMap = GetInverseNodeMap(ways);
        var mergeMap = new Dictionary<CleanWay, CleanWay>();

        foreach (var way in ways)
        {
            // LEFTOFF
        }
        HashSet<CleanWay> __MergeAccountedLookup(ulong nodeId)
        {
            var o = new HashSet<CleanWay>();
            foreach (var originalResult in inverseNodeMap[nodeId])
            {
                var lookupValue = originalResult;
                while (mergeMap.TryGetValue(lookupValue, out var v)) lookupValue = v;
                o.Add(lookupValue);
            }
            return o;
        }

        void __Merge(CleanWay first, CleanWay second)
        {
            var tags = new Dictionary<string, string>(first.Tags);
            foreach (var secondTag in second.Tags) tags.TryAdd(secondTag.Key, secondTag.Value);
            var mergedWay = new CleanWay()
            {
                // new way takes on the Id of the first.
                Id = first.Id,
                Nodes = [.. first.Nodes.Take(first.Nodes.Count - 1), .. second.Nodes.Skip(1)],
                Tags = tags,
            };
            mergeMap[first] = mergedWay;
            mergeMap[second] = mergedWay;
        }
    }
    private RoadSystem()
    {

    }
    public IReadOnlySet<RoadConnection> Connections { get; }
    public IReadOnlySet<RoadNode> Nodes { get; }
    public RawCoordinates ProjectionOrigin { get; }

    private struct XYCoordinates(double X, double Y);

    // Who up delegating they formulas to Claude
    private static (double x, double y, double z) CartesianOnEarthSurface(double latRad, double lonRad)
    {
        return (
            EARTH_RADIUS * Math.Cos(latRad) * Math.Cos(lonRad),
            EARTH_RADIUS * Math.Cos(latRad) * Math.Sin(lonRad),
            EARTH_RADIUS * Math.Sin(latRad)
        );
    }

    private static IEnumerable<XYCoordinates> ProjectAll(
        RawCoordinates origin,
        IEnumerable<RawCoordinates> nodes)
    {
        // Pre-compute the basis vectors once for the whole batch:
        var lat0 = origin.Latitude * Math.PI / 180.0;
        var lon0 = origin.Longitude * Math.PI / 180.0;

        var (x0, y0, z0) = CartesianOnEarthSurface(lat0, lon0);

        var eX = -Math.Sin(lon0);
        var eY = Math.Cos(lon0);

        var nX = -Math.Sin(lat0) * Math.Cos(lon0);
        var nY = -Math.Sin(lat0) * Math.Sin(lon0);
        var nZ = Math.Cos(lat0);

        foreach (var node in nodes)
        {
            var lat = node.Latitude * Math.PI / 180.0;
            var lon = node.Longitude * Math.PI / 180.0;

            var (xP, yP, zP) = CartesianOnEarthSurface(lat, lon);

            var dx = xP - x0;
            var dy = yP - y0;
            var dz = zP - z0;

            yield return new(
                eX * dx + eY * dy,
                nX * dx + nY * dy + nZ * dz
            );
        }
    }
}