namespace CVMatrix.DropOffDefense.SLib.Loc;

public record PlanarCoordinates(double X, double Y)
{
    public static PlanarCoordinates operator +(PlanarCoordinates a) => a;
    public static PlanarCoordinates operator -(PlanarCoordinates a) => new(-a.X, -a.Y);
    public static PlanarCoordinates operator +(PlanarCoordinates a, PlanarCoordinates b) => new(a.X + b.X, a.Y + b.Y);
    public static PlanarCoordinates operator -(PlanarCoordinates a, PlanarCoordinates b) => new(a.X - b.X, a.Y - b.Y);
    public static PlanarCoordinates operator *(PlanarCoordinates a, double scalar) => new(a.X * scalar, a.Y * scalar);
}