namespace CVMatrix.DropOffDefense.SLib.Core;

public enum EIntersectionType
{
    /// <summary>
    /// No restrictions, vehicles pass through node freely.
    /// </summary>
    Free,
    /// <summary>
    /// Traffic must yield to higher priority traffic and wait for a 1s gap. priority is based on min(incomingEdge, targetEdge).
    /// This does not apply to traffic that is staying on the same named road.
    /// </summary>
    Giveway,
    /// <summary>
    /// All traffic must stop for 1s before entering the node, maximum throughput of 1 vehicle per 1.25s
    /// </summary>
    Stop,
    /// <summary>
    /// Traffic from only 1 incoming edge can enter at a time, for a set time per edge.
    /// Incoming edges with no waiting traffic will not be considered.
    /// By default, highest speed limit incoming edge is allowed, until traffic shows up from other edges.
    /// </summary>
    TrafficLight,
    /// <summary>
    /// Same as free, but vehicles must slow down to 50% of the speed limit before entering.
    /// </summary>
    Slow,
}