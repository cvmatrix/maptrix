namespace CVMatrix.DropOffDefense.SLib.Util.ConnectionManagement;

using Loc;
using Loc.Tags;

public interface IEdgeHandle<TNode>
{
    public TNode? To { get; }
    public TNode? From { get; }
}