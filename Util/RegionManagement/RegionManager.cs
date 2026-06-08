namespace CVMatrix.DropOffDefense.SLib.Util.RegionManagement;

using System.Collections;
using System.Numerics;
using ErgoLock;

internal class RegionManager<T> where T : class
{
    private readonly Dictionary<T, LineInfo> _lines = [];
    private readonly Dictionary<T, PointInfo> _points = [];
    private readonly Dictionary<T, RegionInfo> _regions = [];
    private readonly ErgoLock _lock = new();

    public ILineHandle<T> GetLine(T key)
    {
        return new LineHandle(key, this);
    }

    public IPointHandle<T> GetPoint(T key)
    {
        return new PointHandle(key, this);
    }

    public IRegionHandle<T> GetRegion(T key)
    {
        return new RegionHandle(key, this);
    }

    public void RemoveLine(T key)
    {
        using var _ = _lock.WriteScope;
    }

    public void RemovePoint(T key)
    {
        using var _ = _lock.WriteScope;
    }

    public void RemoveRegion(T key)
    {
        using var _ = _lock.WriteScope;
    }

    public void SetLine(T key, IEnumerable<Vector2> path)
    {
        using var _ = _lock.WriteScope;
    }

    public void SetPoint(T key, Vector2 position)
    {
        using var _ = _lock.WriteScope;
    }

    public void SetRegion(T key, IEnumerable<Vector2> boundary, IEnumerable<IEnumerable<Vector2>>? subtractiveBoundaries = null)
    {
        using var _ = _lock.WriteScope;
        RemoveRegion(key);
        var newRegion = new RegionInfo(key)
        {
            BoundaryShape = new()
            {
                Bounds = [..boundary],
                SubtractiveBounds = subtractiveBoundaries?.Select(x => x.ToList()).ToList() ?? [],
            },
        };
        foreach (var existingRegion in _regions.Values)
        {
            if (CheckBoundaryAInsideB(newRegion.BoundaryShape, existingRegion.BoundaryShape))
            {
                newRegion.EncompassedBy.Add(existingRegion.KeyObject);
                existingRegion.Encompasses.Add(newRegion.KeyObject);
            }
            if (CheckBoundaryAInsideB(existingRegion.BoundaryShape, newRegion.BoundaryShape))
            {
                existingRegion.EncompassedBy.Add(newRegion.KeyObject);
                newRegion.Encompasses.Add(existingRegion.KeyObject);
            }
        }

        foreach (var existingLine in _lines.Values)
        {
            if (CheckPathInsideBoundary(existingLine.Path, newRegion.BoundaryShape))
            {
                newRegion.Encompasses
            }
        }
    }

    private LineInfo? GetLineInfo(T key)
    {
        using var _ = _lock.ReadScope;
        return _lines.GetValueOrDefault(key);
    }

    private PointInfo? GetPointInfo(T key)
    {
        using var _ = _lock.ReadScope;
        return _points.GetValueOrDefault(key);
    }

    private RegionInfo? GetRegionInfo(T key)
    {
        using var _ = _lock.ReadScope;
        return _regions.GetValueOrDefault(key);
    }

    // BUG: in the edgecase that B has a subtractive boundaries that overlap area-wise with A but have no verticies within A, this method may incorrectly return true.
    // Can be fixed by doing proper edge-to-edge checking for bound-inside-bound checking, but that would be overkill i think for our use case.
    private static bool CheckBoundaryAInsideB(BoundaryShape a, BoundaryShape b)
    {
        if (!a.Bounds.All(aPoint => CheckPointInsideBoundary(aPoint, b))) return false;
        foreach (var bsSubtractive in b.SubtractiveBounds)
        foreach (var bsPoint in bsSubtractive)
        {
            if (!CheckPointInsideBounds(bsPoint, a.Bounds)) continue;
            if (a.SubtractiveBounds.Any(asBound => CheckPointInsideBounds(bsPoint, asBound))) continue;
            return false;
        }

        return true;
    }

    private static bool CheckPointInsideBoundary(Vector2 point, BoundaryShape boundaryShape)
    {
        if (!CheckPointInsideBounds(point, boundaryShape.Bounds)) return false;
        if (boundaryShape.SubtractiveBounds.Any(bounds => CheckPointInsideBounds(point, bounds))) return false;
        return true;
    }

    // BUG: same bug behavior as CheckBoundaryAInsideB()
    private static bool CheckPathInsideBoundary(IReadOnlyList<Vector2> path, BoundaryShape boundaryShape)
    {
        return path.All(x => CheckPointInsideBoundary(x, boundaryShape));
    }
    private static bool CheckPointInsideBounds(Vector2 point, IReadOnlyList<Vector2> bounds)
    {
        // algo:
        // check for intersections with ray pointing in positive X from point
        // point is inside bounds iff intersection count is odd

        if (bounds.Count < 3) return false;
        var intersections = 0;
        // for zero-checking later:
        foreach (var (rawA, rawB) in IterBoundaryBounds(bounds))
        {
            // get point-relative coordinates:
            var a = rawA - point;
            var b = rawB - point;
            if (a.Y == 0) a += new Vector2(0, float.Epsilon);
            if (b.Y == 0) b += new Vector2(0, float.Epsilon);

            var axSign = Math.Sign(a.X);
            var aySign = Math.Sign(a.Y);
            var bxSign = Math.Sign(b.X);
            var bySign = Math.Sign(b.Y);

            ySigns.Add(aySign);
            if (aySign == bySign ||
                (axSign == -1 && bxSign == -1))
                continue;
            //~ already zero guarded by (aySign == bySign) above
            var inverseSlope = (b.X - a.X) / (b.Y - a.Y);
            if (a.X + -a.Y * inverseSlope < 0) continue;

            intersections++;
        }

        return intersections % 2 != 0;
    }

    private static IEnumerable<(Vector2, Vector2)> IterBoundaryBounds(IEnumerable<Vector2> bounds)
    {
        using var iter = bounds.GetEnumerator();
        if (!iter.MoveNext()) yield break;
        var point = iter.Current;
        var first = point;
        if (!iter.MoveNext()) yield break;
        var next = iter.Current;
        var hasThird = false;
        while (iter.MoveNext())
        {
            yield return (point, next);
            point = next;
            next = iter.Current;
            hasThird = true;
        }

        if (!hasThird) yield break;
        // last point to first point:
        yield return (next, first);
    }

    private abstract class RegionElement(T keyObject)
    {
        public readonly T KeyObject = keyObject;
        public HashSet<T> EncompassedBy { get; set; } = [];
        public HashSet<T> Encompasses { get; set; } = [];
    }

    private class BoundaryShape
    {
        public List<List<Vector2>> SubtractiveBounds { get; set; } = [];
        public List<Vector2> Bounds { get; set; } = [];
    }

    private class LineHandle(T keyObject, RegionManager<T> parent) : ILineHandle<T>
    {
        public readonly RegionManager<T> Parent = parent;
        public readonly T KeyObject = keyObject;
        public IReadOnlyList<Vector2>? Path => Parent.GetLineInfo(KeyObject)?.Path;
        public IReadOnlySet<T> EncompassedBy => Parent.GetLineInfo(KeyObject)?.EncompassedBy ?? [];
        public IReadOnlySet<T> Encompasses => Parent.GetLineInfo(KeyObject)?.Encompasses ?? [];
    }

    private class LineInfo(T keyObject) : RegionElement(keyObject)
    {
        public List<Vector2> Path { get; set; } = [];
    }

    private class PointHandle(T keyObject, RegionManager<T> parent) : IPointHandle<T>
    {
        public readonly RegionManager<T> Parent = parent;
        public readonly T KeyObject = keyObject;
        public IReadOnlySet<T> EncompassedBy => Parent.GetPointInfo(KeyObject)?.EncompassedBy ?? [];
        public IReadOnlySet<T> Encompasses => Parent.GetPointInfo(KeyObject)?.Encompasses ?? [];
        public Vector2? Position => Parent.GetPointInfo(KeyObject)?.Position;
    }

    private class PointInfo(T keyObject) : RegionElement(keyObject)
    {
        public Vector2 Position { get; set; } = default;
    }

    private class RegionHandle(T keyObject, RegionManager<T> parent) : IRegionHandle<T>
    {
        public readonly RegionManager<T> Parent = parent;
        public readonly T KeyObject = keyObject;
        public IReadOnlyList<IReadOnlyList<Vector2>> SubtractiveBoundaries => Parent.GetRegionInfo(KeyObject)?.BoundaryShape.SubtractiveBounds ?? [];
        public IReadOnlyList<Vector2> Boundary => Parent.GetRegionInfo(KeyObject)?.BoundaryShape.Bounds ?? [];
        public IReadOnlySet<T> EncompassedBy => Parent.GetRegionInfo(KeyObject)?.EncompassedBy ?? [];
        public IReadOnlySet<T> Encompasses => Parent.GetRegionInfo(KeyObject)?.Encompasses ?? [];
    }

    private class RegionInfo(T keyObject) : RegionElement(keyObject)
    {
        public BoundaryShape BoundaryShape { get; set; } = new();
    }
}