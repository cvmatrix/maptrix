namespace CVMatrix.DropOffDefense.SLib.Internal;

using Loc;
using Loc.Tags;
using Util.GraphManagement;
using Util.Collections;
internal class Intersection : TaggableMapElement<IIntersectionTag>, ILocIntersection
{
    private INodeHandle<Way> _graphHandle = null!;
    protected override IIntersectionTag? SerializeTag(string key, string value)
    {
        throw new NotImplementedException();
    }

    ILocCoordinates ILocIntersection.Position => throw new NotImplementedException();

    // DEV: mapped view works because uhhhh it's garunteed that the only objects present are exactly the internal type probably uhh.

    IReadOnlySet<ILocWay> ILocIntersection.Incoming => _graphHandle.Incoming.MappedView(ILocWay (x) => x, x => (Way)x);

    IReadOnlySet<ILocWay> ILocIntersection.Outgoing => _graphHandle.Outgoing.MappedView(ILocWay (x) => x, x => (Way)x);

    private INodeHandle<Way> GetHandle()
    {
        _graphHandle = _graphHandle ?? SourceMap.Graph.GetNode(this);
        return _graphHandle;
    }
}