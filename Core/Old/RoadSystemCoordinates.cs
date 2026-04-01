namespace CVMatrix.DropOffDefense.SLib.Core.Old;

using OverpassAPI;

public record RoadSystemCoordinates : IRoadSystemLinked
{
    public required double X { get; init; }
    public required double Y { get; init; }
    public required RawCoordinates Raw { get; init; }
    public required RoadSystem RoadSystem { get; init; }
}