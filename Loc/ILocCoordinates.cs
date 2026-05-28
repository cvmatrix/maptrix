namespace CVMatrix.DropOffDefense.SLib.Loc;

public interface ILocCoordinates
{
    public ProjectionSource Source { get; }
    public PlanarCoordinates Local { get; }
    public RawEarthCoordinates Raw { get; }

}