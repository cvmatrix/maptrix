namespace CVMatrix.DropOffDefense.SLib.Util.GraphManagement;
using Loc;
using Loc.Tags;

public interface IEdgeHandle<TNode>
{
    public TNode? To { get; }
    public TNode? From { get; }
}