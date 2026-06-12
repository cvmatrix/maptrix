namespace CVMatrix.Maptrix.Internal;

using Trix;

internal class Coordinates : ITrixCoordinates
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