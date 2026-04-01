namespace CVMatrix.DropOffDefense.SLib.Core.Stats;

public record TravelWayStats
{
    public required double SpeedLimit { get; init; }
    public required ERoadType RoadType { get; init; }
    public required int LaneCount { get; init; }
}