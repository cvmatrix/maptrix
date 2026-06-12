namespace CVMatrix.Maptrix.Trix;

public interface ITrixCoordinates
{
    public PlanarCoordinates Local { get; }
    public ProjectionSource Source { get; }
    public RawEarthCoordinates Raw { get; }
}