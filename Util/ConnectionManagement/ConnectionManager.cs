namespace CVMatrix.DropOffDefense.SLib.Util.ConnectionManagement;

using Loc;
using Loc.Tags;
using System.Threading;

public class ConnectionManager<TNode, TEdge>
{
    private readonly ReaderWriterLockSlim _lock = new();
    public void Connect(EConnectionDirection direction, TNode node, TEdge edge)
    {
        throw new NotImplementedException();
    }

    public void Disconnect(TNode node, TEdge edge)
    {
        throw new NotImplementedException();
    }

    public void FullyDisconnectNode(TNode node)
    {
        throw new NotImplementedException();
    }

    public void FullyDisconnectEdge(TEdge edge)
    {
        throw new NotImplementedException();
    }

    public void SwapNode(TNode from, TNode to)
    {
        throw new NotImplementedException();
    }

    public void SwapEdge(TEdge from, TEdge to)
    {
        throw new NotImplementedException();
    }
    public INodeHandle<TEdge> GetNode(TNode node)
    {
        throw new NotImplementedException();
    }
    public IEdgeHandle<TNode> GetEdge(TEdge edge)
    {
        throw new NotImplementedException();
    }

}