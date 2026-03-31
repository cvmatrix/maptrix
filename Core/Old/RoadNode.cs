namespace CVMatrix.DropOffDefense.SLib.Core;

public record RoadNode : IRoadSystemLinked
{
    public required RoadSystem RoadSystem { get; init; }
    public required RoadSystemCoordinates Location { get; init; }
    public required IReadOnlySet<RoadConnection> OutgoingConnections { get; init; }
    public required IReadOnlySet<RoadConnection> IncomingConnections { get; init; }
    public required EIntersectionType NodeType { get; init; }
}