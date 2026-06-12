namespace CVMatrix.Maptrix.Util.GraphManagement;

public interface IEdgeHandle<TNode>
{
    public TNode? From { get; }
    public TNode? To { get; }
}