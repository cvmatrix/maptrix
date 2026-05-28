namespace CVMatrix.DropOffDefense.SLib.Util.ConnectionManagement;

using Loc;
using Loc.Tags;
using System.Threading;
using Util.ErgoLock;

public class ConnectionManager<TNode, TEdge>
    where TNode : class
    where TEdge : class
{
    private readonly ErgoLock _lock = new();
    private Dictionary<
    public void Connect(EConnectionDirection direction, TNode node, TEdge edge)
    {
        using var _ = _lock.ReadScope;
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

    private class NodeInfo(TNode node, ConnectionManager<TNode, TEdge> parent)
    {
        public readonly ConnectionManager<TNode, TEdge> Parent = parent;
        public bool IsInsignificant => Incoming.Count == 0 && Outgoing.Count == 0;
        public readonly TNode KeyObject = node;
        public HashSet<TEdge> Incoming { get; set; } = [];
        public HashSet<TEdge> Outgoing { get; set; } = [];
        public NodeHandle Handle() => new(this);
    }

    private class EdgeInfo(TEdge edge)
    {
        public bool HoldsNoData => To is null && From is null;
        public readonly TEdge KeyObject = edge;
        public TNode? To { get; set; } = null;
        public TNode? From { get; set; } = null;
    }

    private class NodeHandle(NodeInfo info) : INodeHandle<TEdge>
    {
        public readonly NodeInfo InfoObject = info;
        private readonly ErgoLock _lock = info.Parent._lock;

        public IReadOnlySet<TEdge> Incoming => _lock.InReadScope(() => InfoObject.Incoming);
        public IReadOnlySet<TEdge> Outgoing => _lock.InReadScope(() => InfoObject.Outgoing);

        ~NodeHandle()
        {
            if (InfoObject.IsInsignificant)
            {
                // remove this node from data
                throw new NotImplementedException();
            }
        }
    }
}