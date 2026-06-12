namespace CVMatrix.Maptrix.Internal;

using Loc;

internal class Coordinates : ILocCoordinates
{
    public PlanarCoordinates Local { get; }
    public ProjectionSource Source { get; }
    public RawEarthCoordinates Raw { get; }

    public Coordinates(ProjectionSource projection, RawEarthCoordinates rawCoordinates)
    {
        Source = projection;
        Raw = rawCoordinates;
        Local = projection.ProjectPoint(rawCoordinates);
    }
}