namespace CVMatrix.DropOffDefense.SLib.Internal;

using Loc;
using Loc.Tags;
using System.Numerics;

internal class Way : TaggableMapElement<IWayTag>, ILocWay
{
    public float PathLength { get; private set; }
    public required int WayConnectionId { get; set; }
    public Way? AdjacentReverse { get; private set; }

    public required IReadOnlyList<Coordinates> Path
    {
        get => _path;
        set
        {
            _path = value;
            PathLength = GetPathLength(_path);
        }
    }

    private IReadOnlyList<Coordinates> _path = [];
    float ILocWay.PathLength => PathLength;

    ILocIntersection ILocWay.From => throw new NotImplementedException();

    ILocIntersection ILocWay.To => throw new NotImplementedException();

    ILocWay? ILocWay.AdjacentReverse => AdjacentReverse;
    int ILocWay.WayConnectionId => WayConnectionId;

    IReadOnlyList<ILocCoordinates> ILocWay.Path => Path;

    public Way? BreakFromAdjacentReverse()
    {
        var formerReverse = AdjacentReverse;
        if (formerReverse is null) return null;
        AdjacentReverse = null;
        formerReverse.AdjacentReverse = null;
        return formerReverse;
    }

    public Way CreateAdjacentReverse()
    {
        Way reverseWay = null!;
        throw new NotImplementedException();

        if (AdjacentReverse is { } existingReverse)
            existingReverse.AdjacentReverse = null;
        AdjacentReverse = reverseWay;
        reverseWay.AdjacentReverse = this;
        return reverseWay;
    }

    protected override IWayTag? SerializeTag(string key, string value)
    {
        throw new NotImplementedException();
    }

    private static float GetPathLength(IEnumerable<Coordinates> path)
    {
        float o = 0;
        using var iter = path.GetEnumerator();
        if (!iter.MoveNext()) return o;
        var lastPoint = iter.Current.Local;
        while (iter.MoveNext())
        {
            var point = iter.Current.Local;
            o += Vector2.Distance(lastPoint, point);
            lastPoint = point;
        }

        return o;
    }
}