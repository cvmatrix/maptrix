namespace CVMatrix.Maptrix.Util.GraphManagement;

using Loc;
using Loc.Tags;

public interface INodeHandle<TEdge>
{
    public IReadOnlySet<TEdge> Incoming { get; }
    public IReadOnlySet<TEdge> Outgoing { get; }
}