namespace CVMatrix.DropOffDefense.SLib.Loc;
using System.Numerics;
public record PlanarCoordinates(float X, float Y)
{
    public static PlanarCoordinates operator +(PlanarCoordinates a) => a;
    public static PlanarCoordinates operator -(PlanarCoordinates a) => new(-a.X, -a.Y);
    public static PlanarCoordinates operator +(PlanarCoordinates a, PlanarCoordinates b) => new(a.X + b.X, a.Y + b.Y);
    public static PlanarCoordinates operator -(PlanarCoordinates a, PlanarCoordinates b) => new(a.X - b.X, a.Y - b.Y);
    public static PlanarCoordinates operator *(PlanarCoordinates a, float scalar) => new(a.X * scalar, a.Y * scalar);
    public static implicit operator Vector2(PlanarCoordinates coordinates) => new(coordinates.X, coordinates.Y);
    public static implicit operator PlanarCoordinates(Vector2 vector) => new(vector.X, vector.Y);
    public Vector2 AsVector() => this;
}