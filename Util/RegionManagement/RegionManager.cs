namespace CVMatrix.DropOffDefense.SLib.Util.RegionManagement;

using System.Collections;
using System.Numerics;
using ErgoLock;
internal class RegionManager<T> where T : class
{
    private readonly ErgoLock _lock = new();
    public void SetRegion(T key, IEnumerable<Vector2> boundary, IEnumerable<IEnumerable<Vector2>>? subtractiveBoundaries = null)
    {
        using var _ = _lock.WriteScope;
    }

    public void SetRegion(T key, IEnumerable<IEnumerable<Vector2>> convexBoundaries)
    {

    }
    public void RemoveRegion(T key)
    {
        using var _ = _lock.WriteScope;
    }

    public IRegionHandle<T> GetRegion(T key)
    {

    }


    private class Boundary
    {
        public required List<Vector2> Bounds { get; set; }
        public required List<List<Vector2>> SubtractiveBounds { get; set; }
    }

    private static bool CheckPointInsideBoundary(Vector2 point, Boundary boundary)
    {
        if (!CheckPointInsideBounds(point, boundary.Bounds)) return false;
        if (boundary.SubtractiveBounds.Any(bounds => CheckPointInsideBounds(point, bounds))) return false;
        return true;
    }

    private static bool CheckPointInsideBounds(Vector2 point, IReadOnlyList<Vector2> bounds)
    {
        // algo:
        // check for intersections with ray pointing in positive X from point
        // point is inside bounds iff intersection count is odd

        if (bounds.Count < 3) return false;
        int intersections = 0;
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
            if ((aySign == bySign) ||
                (axSign == -1 && bxSign == -1))
                continue;
            //~ already zero guarded by (aySign == bySign) above
            var inverseSlope = (b.X - a.X) / (b.Y - a.Y);
            if (a.X + (-a.Y * inverseSlope) < 0) continue;

            intersections++;
        }

        return intersections % 2 != 0;
    }

    // BUG: in the edgecase that B has a subtractive boundaries that overlap area-wise with A but have no verticies within A, this method may incorrectly return true.
    // Can be fixed by doing proper edge-to-edge checking for bound-inside-bound checking, but that would be overkill i think for our use case.
    private static bool CheckBoundaryAInsideB(Boundary a, Boundary b)
    {
        if (!a.Bounds.All(aPoint => CheckPointInsideBoundary(aPoint, b))) return false;
        foreach (var bsSubtractive in b.SubtractiveBounds)
        {
            foreach (var bsPoint in bsSubtractive)
            {
                if (!CheckPointInsideBounds(bsPoint, a.Bounds)) continue;
                if (a.SubtractiveBounds.Any(asBound => CheckPointInsideBounds(bsPoint, asBound))) continue;
                return false;
            }
        }

        return true;
    }

    private static IEnumerable<(Vector2, Vector2)> IterBoundaryBounds(IEnumerable<Vector2> bounds)
    {
        using var iter = bounds.GetEnumerator();
        if (!iter.MoveNext()) yield break;
        var point = iter.Current;
        var first = point;
        if (!iter.MoveNext()) yield break;
        var next = iter.Current;
        bool hasThird = false;
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
}