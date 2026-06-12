namespace CVMatrix.Maptrix.Util.GraphManagement;

public interface INodeHandle<TEdge>
{
    public IReadOnlySet<TEdge> Incoming { get; }
    public IReadOnlySet<TEdge> Outgoing { get; }
}