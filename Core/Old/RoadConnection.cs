namespace CVMatrix.DropOffDefense.SLib.Core.Old;

public record RoadConnection : IRoadSystemLinked
{
    public required ERoadConnectionType ConnectionType { get; init; }

    // in meters probably or something.
    public required float Distance { get; init; }
    public required int LaneCount { get; init; }
    public required int SpeedLimit { get; init; }
    public required IReadOnlyList<RoadSystemCoordinates> PathTrace { get; init; }
    public required RoadNode From { get; init; }
    public required RoadNode To { get; init; }
    public required RoadSystem RoadSystem { get; init; }
    public required UniqueRoadIdentifier Identifier { get; init; }
}