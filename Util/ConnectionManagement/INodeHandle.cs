namespace CVMatrix.DropOffDefense.SLib.Util.ConnectionManagement;

using Loc;
using Loc.Tags;

public interface INodeHandle<TEdge>
{
    public IReadOnlySet<TEdge> Incoming { get; }
    public IReadOnlySet<TEdge> Outgoing { get; }
}