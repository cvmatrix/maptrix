namespace CVMatrix.DropOffDefense.SLib.Util.GraphManagement;

using Loc;
using Loc.Tags;
using System.Threading;
using ErgoLock;

public class GraphManager<TNode, TEdge>
    where TNode : class
    where TEdge : class
{
    private readonly ErgoLock _lock = new();
    private readonly Dictionary<TEdge, EdgeInfo> _edges = [];
    private readonly Dictionary<TNode, NodeInfo> _nodes = [];

    public void Connect(EConnectionDirection direction, TNode node, TEdge edge)
    {
        using var _ = _lock.WriteScope;
        _edges.TryAdd(edge, new EdgeInfo(edge, this));
        _nodes.TryAdd(node, new NodeInfo(node, this));
        var edgeInfo = _edges[edge];
        var nodeInfo = _nodes[node];

        switch (direction)
        {
            case EConnectionDirection.To:
                if (edgeInfo.To == node) return;
                if (edgeInfo.To is { } existingToKey)
                {
                    var existingTo = _nodes[existingToKey];
                    existingTo.Incoming.Remove(edge);
                    if (existingTo.IsInsignificant) _nodes.Remove(existingToKey);
                }
                edgeInfo.To = node;
                nodeInfo.Incoming.Add(edge);
                break;

            case EConnectionDirection.From:
                if (edgeInfo.From == node) return;
                if (edgeInfo.From is { } existingFromKey)
                {
                    var existingTo = _nodes[existingFromKey];
                    existingTo.Incoming.Remove(edge);
                    if (existingTo.IsInsignificant) _nodes.Remove(existingFromKey);
                }
                edgeInfo.From = node;
                nodeInfo.Outgoing.Add(edge);
                break;
            default: throw new NotSupportedException();
        }
    }

    public void Disconnect(TNode node, TEdge edge)
    {
        using var _ = _lock.WriteScope;

        if (!_nodes.TryGetValue(node, out var nodeInfo)) return;
        if (!_edges.TryGetValue(edge, out var edgeInfo)) return;

        if (edgeInfo.To == node)
        {
            edgeInfo.To = null;
            nodeInfo.Incoming.Remove(edge);
        }
        if (edgeInfo.From == node)
        {
            edgeInfo.From = null;
            nodeInfo.Outgoing.Remove(edge);
        }

        if (edgeInfo.IsInsignificant) _edges.Remove(edge);
        if (nodeInfo.IsInsignificant) _nodes.Remove(node);
    }

    public void FullyDisconnectEdge(TEdge edge)
    {
        using var _ = _lock.WriteScope;

        if (_edges.GetValueOrDefault(edge) is not { } info) return;

        if (info.To is { } toKey)
        {
            var node = _nodes[toKey];
            node.Incoming.Remove(edge);
            if (node.IsInsignificant) _nodes.Remove(toKey);
        }
        if (info.From is { } fromKey)
        {
            var node = _nodes[fromKey];
            node.Outgoing.Remove(edge);
            if (node.IsInsignificant) _nodes.Remove(fromKey);
        }
        _edges.Remove(edge);
    }

    public void FullyDisconnectNode(TNode node)
    {
        using var _ = _lock.WriteScope;

        if (_nodes.GetValueOrDefault(node) is not { } info) return;
        foreach (var edgeKey in info.Incoming)
        {
            var edge = _edges[edgeKey];
            edge.To = null;
            if (edge.IsInsignificant) _edges.Remove(edgeKey);
        }
        foreach (var edgeKey in info.Outgoing)
        {
            var edge = _edges[edgeKey];
            edge.From = null;
            if (edge.IsInsignificant) _edges.Remove(edgeKey);
        }
        _nodes.Remove(node);
    }

    public IEdgeHandle<TNode> GetEdge(TEdge edge) => new EdgeHandle(edge, this);
    public INodeHandle<TEdge> GetNode(TNode node) => new NodeHandle(node, this);

    protected EdgeInfo? GetEdgeInfo(TEdge edge)
    {
        using var _ = _lock.ReadScope;
        return _edges.GetValueOrDefault(edge);
    }
    protected NodeInfo? GetNodeInfo(TNode node)
    {
        using var _ = _lock.ReadScope;
        return _nodes.GetValueOrDefault(node);
    }
    public void SwapEdge(TEdge oldEdge, TEdge newEdge)
    {
        using var _ = _lock.WriteScope;
        if (!_edges.Remove(oldEdge, out var oldInfo)) return;
        oldInfo.KeyObject = newEdge;
        _edges[newEdge] = oldInfo;
    }

    public void SwapNode(TNode oldNode, TNode newNode)
    {
        using var _ = _lock.WriteScope;
        if (!_nodes.Remove(oldNode, out var oldInfo)) return;
        oldInfo.KeyObject = newNode;
        _nodes[newNode] = oldInfo;
    }

    protected class EdgeHandle(TEdge node, GraphManager<TNode, TEdge> parent) : IEdgeHandle<TNode>
    {
        public readonly GraphManager<TNode, TEdge> Parent = parent;
        public readonly TEdge KeyObject = node;
        public TNode? From => Parent.GetEdgeInfo(KeyObject)?.From;
        public TNode? To => Parent.GetEdgeInfo(KeyObject)?.To;

    }

    protected class EdgeInfo(TEdge edge, GraphManager<TNode, TEdge> parent)
    {
        public readonly GraphManager<TNode, TEdge> Parent = parent;
        public TEdge KeyObject = edge;
        public TNode? From { get; set; } = null;
        public TNode? To { get; set; } = null;
        public bool IsInsignificant => To is null && From is null;
    }

    protected class NodeHandle(TNode node, GraphManager<TNode, TEdge> parent) : INodeHandle<TEdge>
    {
        public readonly GraphManager<TNode, TEdge> Parent = parent;
        public readonly TNode KeyObject = node;
        public IReadOnlySet<TEdge> Incoming => Parent.GetNodeInfo(KeyObject)?.Incoming ?? [];
        public IReadOnlySet<TEdge> Outgoing => Parent.GetNodeInfo(KeyObject)?.Outgoing ?? [];
    }

    protected class NodeInfo(TNode node, GraphManager<TNode, TEdge> parent)
    {
        public readonly GraphManager<TNode, TEdge> Parent = parent;
        public TNode KeyObject = node;
        public HashSet<TEdge> Incoming { get; set; } = [];
        public HashSet<TEdge> Outgoing { get; set; } = [];
        public bool IsInsignificant => Incoming.Count == 0 && Outgoing.Count == 0;
    }
}