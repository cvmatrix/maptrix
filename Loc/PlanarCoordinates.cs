namespace CVMatrix.DropOffDefense.SLib.Loc;
using System.Numerics;
public record PlanarCoordinates(double X, double Y)
{
    public static PlanarCoordinates operator +(PlanarCoordinates a) => a;
    public static PlanarCoordinates operator -(PlanarCoordinates a) => new(-a.X, -a.Y);
    public static PlanarCoordinates operator +(PlanarCoordinates a, PlanarCoordinates b) => new(a.X + b.X, a.Y + b.Y);
    public static PlanarCoordinates operator -(PlanarCoordinates a, PlanarCoordinates b) => new(a.X - b.X, a.Y - b.Y);
    public static PlanarCoordinates operator *(PlanarCoordinates a, double scalar) => new(a.X * scalar, a.Y * scalar);
    public static implicit operator Vector2(PlanarCoordinates coordinates) => new((float)coordinates.X, (float)coordinates.Y);
}