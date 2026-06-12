namespace CVMatrix.Maptrix.Trix;

using System.Numerics;

public record PlanarCoordinates(float X, float Y)
{
    public static PlanarCoordinates operator +(PlanarCoordinates a, PlanarCoordinates b)
    {
        return new(a.X + b.X, a.Y + b.Y);
    }

    public static implicit operator Vector2(PlanarCoordinates coordinates)
    {
        return new(coordinates.X, coordinates.Y);
    }

    public static implicit operator PlanarCoordinates(Vector2 vector)
    {
        return new(vector.X, vector.Y);
    }

    public static PlanarCoordinates operator *(PlanarCoordinates a, float scalar)
    {
        return new(a.X * scalar, a.Y * scalar);
    }

    public static PlanarCoordinates operator -(PlanarCoordinates a, PlanarCoordinates b)
    {
        return new(a.X - b.X, a.Y - b.Y);
    }

    public static PlanarCoordinates operator -(PlanarCoordinates a)
    {
        return new(-a.X, -a.Y);
    }

    public static PlanarCoordinates operator +(PlanarCoordinates a)
    {
        return a;
    }

    public Vector2 AsVector()
    {
        return this;
    }
}