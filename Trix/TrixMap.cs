namespace CVMatrix.Maptrix.Trix;

using System.Numerics;
using CVMatrix.Maptrix.Internal;
using CVMatrix.Maptrix.OverpassAPI.Model.Clean;
using CVMatrix.Maptrix.Util.Collections;
using CVMatrix.Maptrix.Util.GraphManagement;
using GraphManager = Util.GraphManagement.GraphManager<Internal.Intersection, Internal.Way>;
using RegionManager = Util.RegionManagement.RegionManager<Internal.Region, Internal.Way, Internal.IPointElement>;
public class TrixMap
{

    internal Resources Data;
    public ProjectionSource Projection => Data.Projection;
    public IReadOnlySet<ITrixRegion> Regions => Data.Regions.CastingView<Region, ITrixRegion>();
    public IReadOnlySet<ITrixIntersection> Intersections => Data.Intersections.CastingView<Intersection, ITrixIntersection>();
    public IReadOnlySet<ITrixWay> Ways => Data.Ways.CastingView<Way, ITrixWay>();
    public IReadOnlySet<ITrixPoi> Pois => Data.Pois.CastingView<Poi, ITrixPoi>();

    private TrixMap(Resources data)
    {
        Data = data;
    }

    public static TrixMap FromOverpassData(CleanData data, ProjectionSource projection)
    {
        HashSet<CleanWayId> interpretRegions = [];
        HashSet<CleanWayId> interpretWays = [];
        HashSet<CleanNodeId> interpretPois = [..data.Nodes.Keys];
        Dictionary<CleanNodeId, List<CleanWayId>> interpretWayPathMap = [];
        HashSet<CleanNodeId> interpretWayEnds = [];
        Dictionary<CleanRelationId, (CleanWayId, List<CleanWayId>)> interpretMultipolygonRegions = [];

        // ways/regions:
        foreach (var wayData in data.Ways.Values)
        {
            // remove nodes from pois:
            interpretPois.ExceptWith(wayData.Nodes);

            // region check:
            if (wayData.Nodes[0] == wayData.Nodes[^1])
            {
                interpretRegions.Add(wayData.Id);
                continue;
            }

            interpretWays.Add(wayData.Id);
            foreach (var wayNode in wayData.Nodes)
            {
                interpretWayPathMap.GetOrInitialize(wayNode, new List<CleanWayId>(1)).Add(wayData.Id);
            }
            interpretWayEnds.Add(wayData.Nodes[0]);
            interpretWayEnds.Add(wayData.Nodes[^1]);
        }

        // multipolygon regions:
        foreach (var relationData in data.Relations.Values)
        {
            if (!(relationData.Tags.TryGetValue("type", out var typeValue) && typeValue == "multipolygon")) continue;
            CleanWayId? outer = null;
            List<CleanWayId> inner = [];
            foreach (var member in relationData.Members)
            {
                if (member.Type != CleanRelationMember.ERelationType.Way) continue;
                // relation members should not be handled individually:
                interpretRegions.Remove(member.Ref);
                switch (member.Role)
                {
                    case "outer":
                        outer = member.Ref;
                        break;
                    case "inner":
                        inner.Add(member.Ref);
                        break;
                    default:
                        throw new NotSupportedException("Unexpected member role in multipolygon relation: " + member.Role);
                }
            }

            if (outer == null) throw new("Multipolygon region had no member with role 'outer'");
            interpretMultipolygonRegions.Add(relationData.Id, (outer.Value, inner));
        }

        Resources loc = new()
        {
            Projection = projection,
            GraphManager = new(),
            RegionManager = new(),
            Regions = [],
            Intersections = [],
            Pois = [],
            Ways = [],
        };
        TrixMap trixMap = new(loc);

        // simple loc regions:
        foreach (var regionData in interpretRegions.Select(x => data.Ways[x]))
        {
            __AddRegion(regionData);
        }
        // multipolygon regions:
        foreach (var (relationId, (outer, inners)) in interpretMultipolygonRegions)
        {
            var regionObj = __AddRegion(data.Ways[outer], inners.Select(x => data.Ways[x]));
            regionObj.RawTags = data.Relations[relationId].Tags;
        }

        // pois:
        foreach (var poiData in interpretPois.Select(x => data.Nodes[x]))
        {
            __AddPoi(poiData);
        }

        Dictionary<CleanNodeId, Intersection> intersectionMap = [];
        var intersectingNodeList = interpretWayPathMap.Where(x => x.Value.Count > 1).Select(x => x.Key).ToList();

        // populate intersectionMap:
        foreach (var nodeData in intersectingNodeList.Concat(interpretWayEnds).Select(x => data.Nodes[x]))
        {
            if (intersectionMap.ContainsKey(nodeData.Id))
                continue;
            intersectionMap[nodeData.Id] = __AddIntersection(nodeData);
        }

        // make & connect ways:
        foreach (var wayData in interpretWays.Select(x => data.Ways[x]))
        {
            List<Coordinates> currentPath = new(wayData.Nodes.Count)
            {
                __NodeIdCoordinates(wayData.Nodes[0])
            };
            var currentFrom = intersectionMap[wayData.Nodes[0]];
            for (int i = 1; i < wayData.Nodes.Count; i++)
            {
                var pathNode = wayData.Nodes[i];
                currentPath.Add(__NodeIdCoordinates(pathNode));

                // check if this node is an intersection:
                if (!intersectionMap.TryGetValue(pathNode, out var currentTo))
                    continue;
                // then connect, and act as a new way starting from where the last one ended:
                __ConnectWithWay(currentFrom, currentTo, currentPath, wayData.Tags);
                currentPath.RemoveRange(0, currentPath.Count - 1);
                currentFrom = currentTo;
            }
            // ~ above algorithm should always end on an intersection, so shouldnt need to make final connection.

        }

        return trixMap;

        Poi __AddPoi(CleanNode nodeData)
        {
            var poiObj = new Poi()
            {
                Position = __NodeCoordinates(nodeData),
                RawTags = nodeData.Tags,
                SourceMap = trixMap,
            };
            loc.Pois.Add(poiObj);
            loc.RegionManager.SetPoint(poiObj, poiObj.Position.Local.AsVector());
            return poiObj;
        }
        (Way, Way?) __ConnectWithWay(Intersection from, Intersection to, IReadOnlyList<Coordinates> path, IReadOnlyDictionary<string, string> tags)
        {
            var wayObj = new Way()
            {
                RawTags = tags,
                Path = path,
                SourceMap = trixMap,
            };
            loc.Ways.Add(wayObj);
            loc.RegionManager.SetLine(wayObj, wayObj.Path.Select(x => x.Local.AsVector()));
            loc.GraphManager.Connect(EConnectionDirection.From, from, wayObj);
            loc.GraphManager.Connect(EConnectionDirection.To, to, wayObj);
            Way? reverseAdjacentWayObj = null;
            if (!(tags.TryGetValue("oneway", out var v) && v == "yes"))
                reverseAdjacentWayObj = wayObj.CreateAdjacentReverse();
            return (wayObj, reverseAdjacentWayObj);
        }
        Intersection __AddIntersection(CleanNode nodeData)
        {
            var intersectionObj = new Intersection()
            {
                SourceMap = trixMap,
                Position = __NodeCoordinates(nodeData),
                RawTags = nodeData.Tags,
            };
            loc.Intersections.Add(intersectionObj);
            loc.RegionManager.SetPoint(intersectionObj, intersectionObj.Position.Local);
            return intersectionObj;
        }
        Region __AddRegion(CleanWay outerRegionData, IEnumerable<CleanWay>? innerRegionDatas = null)
        {
            var regionObj = new Region()
            {
                SourceMap = trixMap,
                RawTags = outerRegionData.Tags,
                Boundary = __NodesToCoordinates(outerRegionData.Nodes),
                SubtractiveBoundaries = 
                    innerRegionDatas?
                        .Select(innerData => __NodesToCoordinates(innerData.Nodes))
                        .ToList() ?? []
            };
            loc.Regions.Add(regionObj);
            loc.RegionManager.SetRegion(regionObj, regionObj.Boundary.Select(x => (Vector2)x.Local), regionObj.SubtractiveBoundaries.Select(x => x.Select(y => (Vector2)y.Local)));
            return regionObj;
        }

        Coordinates __NodeIdCoordinates(CleanNodeId nodeId)
        {
            var nodeData = data.Nodes[nodeId];
            return new(projection, new(nodeData.Latitude, nodeData.Longitude));
        }
        Coordinates __NodeCoordinates(CleanNode nodeData)
        {
            return new Coordinates(projection, new(nodeData.Latitude, nodeData.Longitude));
        }
        List<Coordinates> __NodesToCoordinates(IEnumerable<CleanNodeId> nodes)
        {
            return nodes.Select(__NodeIdCoordinates).ToList();
        }
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