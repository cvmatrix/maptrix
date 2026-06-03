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

    public required Coordinates Position { get; set; }
    public IReadOnlySet<Way> Incoming => GetHandle().Incoming;
    public IReadOnlySet<Way> Outgoing => GetHandle().Outgoing;
    ILocCoordinates ILocIntersection.Position => Position;

    // DEV: mapped view works because uhhhh it's garunteed that the only objects present are exactly the internal type probably uhh.

    IReadOnlySet<ILocWay> ILocIntersection.Incoming => Incoming.MappedView(ILocWay (x) => x, x => (Way)x);
    IReadOnlySet<ILocWay> ILocIntersection.Outgoing => Outgoing.MappedView(ILocWay (x) => x, x => (Way)x);

    private INodeHandle<Way> GetHandle()
    {
        _graphHandle ??= SourceMap.Graph.GetNode(this);
        return _graphHandle;
    }
}