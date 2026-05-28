namespace CVMatrix.DropOffDefense.SLib.Internal;

using Loc;
using Loc.Tags;
internal class Intersection : TaggableMapElement<IIntersectionTag>, ILocIntersection
{
    protected override IIntersectionTag? SerializeTag(string key, string value)
    {
        throw new NotImplementedException();
    }

    ILocCoordinates ILocIntersection.Position => throw new NotImplementedException();

    IReadOnlySet<ILocWay> ILocIntersection.Incoming => throw new NotImplementedException();

    IReadOnlySet<ILocWay> ILocIntersection.Outgoing => throw new NotImplementedException();
}