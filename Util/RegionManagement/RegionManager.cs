namespace CVMatrix.DropOffDefense.SLib.Util.RegionManagement;

using System.Collections;
using System.Numerics;
using ErgoLock;

internal class RegionManager<TRegion, TLine, TPoint> where TRegion : class where TLine : class where TPoint : class
{
    private readonly Dictionary<TLine, LineInfo> _lines = [];
    private readonly Dictionary<TPoint, PointInfo> _points = [];
    private readonly Dictionary<TRegion, RegionInfo> _regions = [];
    private readonly ErgoLock _lock = new();

    public ILineHandle<TRegion> GetLine(TLine key)
    {
        return new LineHandle(key, this);
    }

    public IPointHandle<TRegion> GetPoint(TPoint key)
    {
        return new PointHandle(key, this);
    }

    public IRegionHandle<TRegion, TLine, TPoint> GetRegion(TRegion key)
    {
        return new RegionHandle(key, this);
    }

    public void RemoveLine(TLine key)
    {
        using var _ = _lock.WriteScope;
        if (!_lines.Remove(key, out var removing)) return;
        foreach (var existingRegion in _regions.Values)
        {
            existingRegion.EncompassesLines.Remove(removing.KeyObject);
        }
    }

    public void RemovePoint(TPoint key)
    {
        using var _ = _lock.WriteScope;
        if (!_points.Remove(key, out var removing)) return;
        foreach (var existingRegion in _regions.Values)
        {
            existingRegion.EncompassesPoints.Remove(removing.KeyObject);
        }
    }

    public void RemoveRegion(TRegion key)
    {
        using var _ = _lock.WriteScope;
        if (!_regions.Remove(key, out var removing)) return;
        foreach (var existingRegion in _regions.Values)
        {
            existingRegion.EncompassesRegions.Remove(removing.KeyObject);
            existingRegion.EncompassedBy.Remove(removing.KeyObject);
        }
        foreach (var existingLine in _lines.Values)
        {
            existingLine.EncompassedBy.Remove(removing.KeyObject);
        }
        foreach (var existingPoint in _points.Values)
        {
            existingPoint.EncompassedBy.Remove(removing.KeyObject);
        }
    }

    public void SetLine(TLine key, IEnumerable<Vector2> path)
    {
        using var _ = _lock.WriteScope;
        RemoveLine(key);
        var newLine = new LineInfo(key)
        {
            Path = [..path],
        };
        foreach (var existingRegion in _regions.Values)
        {
            if (!CheckPathInsideBoundary(newLine.Path, existingRegion.BoundaryShape)) continue;
            newLine.EncompassedBy.Add(existingRegion.KeyObject);
            existingRegion.EncompassesLines.Add(newLine.KeyObject);
        }

        _lines[newLine.KeyObject] = newLine;
    }

    public void SetPoint(TPoint key, Vector2 position)
    {
        using var _ = _lock.WriteScope;
        RemovePoint(key);
        var newPoint = new PointInfo(key)
        {
            Position = position,
        };
        foreach (var existingRegion in _regions.Values)
        {
            if (!CheckPointInsideBoundary(newPoint.Position, existingRegion.BoundaryShape)) continue;
            newPoint.EncompassedBy.Add(existingRegion.KeyObject);
            existingRegion.EncompassesPoints.Add(newPoint.KeyObject);
        }

        _points[newPoint.KeyObject] = newPoint;
    }

    public void SetRegion(TRegion key, IEnumerable<Vector2> boundary, IEnumerable<IEnumerable<Vector2>>? subtractiveBoundaries = null)
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
                existingRegion.EncompassesRegions.Add(newRegion.KeyObject);
            }
            if (CheckBoundaryAInsideB(existingRegion.BoundaryShape, newRegion.BoundaryShape))
            {
                existingRegion.EncompassedBy.Add(newRegion.KeyObject);
                newRegion.EncompassesRegions.Add(existingRegion.KeyObject);
            }
        }

        foreach (var existingLine in _lines.Values)
        {
            if (!CheckPathInsideBoundary(existingLine.Path, newRegion.BoundaryShape)) continue;
            existingLine.EncompassedBy.Add(newRegion.KeyObject);
            newRegion.EncompassesLines.Add(existingLine.KeyObject);
        }
        foreach (var existingPoint in _points.Values)
        {
            if (!CheckPointInsideBoundary(existingPoint.Position, newRegion.BoundaryShape)) continue;
            existingPoint.EncompassedBy.Add(newRegion.KeyObject);
            newRegion.EncompassesPoints.Add(existingPoint.KeyObject);
        }
        _regions[newRegion.KeyObject] = newRegion;
    }

    private LineInfo? GetLineInfo(TLine key)
    {
        using var _ = _lock.ReadScope;
        return _lines.GetValueOrDefault(key);
    }

    private PointInfo? GetPointInfo(TPoint key)
    {
        using var _ = _lock.ReadScope;
        return _points.GetValueOrDefault(key);
    }

    private RegionInfo? GetRegionInfo(TRegion key)
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

    private abstract class RegionElement<TKey>(TKey keyObject) where TKey : class
    {
        public readonly TKey KeyObject = keyObject;
        public HashSet<TRegion> EncompassedBy { get; set; } = [];
    }

    private class BoundaryShape
    {
        public List<List<Vector2>> SubtractiveBounds { get; set; } = [];
        public List<Vector2> Bounds { get; set; } = [];
    }

    private class LineHandle(TLine keyObject, RegionManager<TRegion, TLine, TPoint> parent) : ILineHandle<TRegion>
    {
        public readonly RegionManager<TRegion, TLine, TPoint> Parent = parent;
        public readonly TLine KeyObject = keyObject;
        public IReadOnlyList<Vector2>? Path => Parent.GetLineInfo(KeyObject)?.Path;
        public IReadOnlySet<TRegion> EncompassedBy => Parent.GetLineInfo(KeyObject)?.EncompassedBy ?? [];
    }

    private class LineInfo(TLine keyObject) : RegionElement<TLine>(keyObject)
    {
        public List<Vector2> Path { get; set; } = [];
    }

    private class PointHandle(TPoint keyObject, RegionManager<TRegion, TLine, TPoint> parent) : IPointHandle<TRegion>
    {
        public readonly RegionManager<TRegion, TLine, TPoint> Parent = parent;
        public readonly TPoint KeyObject = keyObject;
        public IReadOnlySet<TRegion> EncompassedBy => Parent.GetPointInfo(KeyObject)?.EncompassedBy ?? [];
        public Vector2? Position => Parent.GetPointInfo(KeyObject)?.Position;
    }

    private class PointInfo(TPoint keyObject) : RegionElement<TPoint>(keyObject)
    {
        public Vector2 Position { get; set; } = default;
    }

    private class RegionHandle(TRegion keyObject, RegionManager<TRegion, TLine, TPoint> parent) : IRegionHandle<TRegion, TLine, TPoint>
    {
        public readonly RegionManager<TRegion, TLine, TPoint> Parent = parent;
        public readonly TRegion KeyObject = keyObject;
        public IReadOnlyList<IReadOnlyList<Vector2>> SubtractiveBoundaries => Parent.GetRegionInfo(KeyObject)?.BoundaryShape.SubtractiveBounds ?? [];
        public IReadOnlyList<Vector2> Boundary => Parent.GetRegionInfo(KeyObject)?.BoundaryShape.Bounds ?? [];
        public IReadOnlySet<TRegion> EncompassedBy => Parent.GetRegionInfo(KeyObject)?.EncompassedBy ?? [];
        public IReadOnlySet<TRegion> EncompassesRegions => Parent.GetRegionInfo(KeyObject)?.EncompassesRegions ?? [];
        public IReadOnlySet<TLine> EncompassesLines => Parent.GetRegionInfo(KeyObject)?.EncompassesLines ?? [];
        public IReadOnlySet<TPoint> EncompassesPoints => Parent.GetRegionInfo(KeyObject)?.EncompassesPoints ?? [];
    }

    private class RegionInfo(TRegion keyObject) : RegionElement<TRegion>(keyObject)
    {
        public BoundaryShape BoundaryShape { get; set; } = new();
        public HashSet<TRegion> EncompassesRegions { get; set; } = [];
        public HashSet<TLine> EncompassesLines { get; set; } = [];
        public HashSet<TPoint> EncompassesPoints { get; set; } = [];
    }
}