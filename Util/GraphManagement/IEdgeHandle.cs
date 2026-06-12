namespace CVMatrix.Maptrix.Util.GraphManagement;

using Loc;
using Loc.Tags;

public interface IEdgeHandle<TNode>
{
    public TNode? From { get; }
    public TNode? To { get; }
}