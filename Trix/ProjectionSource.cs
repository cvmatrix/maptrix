namespace CVMatrix.Maptrix.Trix;

public record ProjectionSource
{
    public const double EARTH_RADIUS_FEET = 20_902_230.0;
    public const double EARTH_RADIUS_METERS = 6_371_000.0;
    public double EarthRadius { get; }
    public RawEarthCoordinates Origin { get; }
    private readonly OriginData _originData;

    public ProjectionSource(RawEarthCoordinates origin, double earthRadius)
    {
        Origin = origin;
        EarthRadius = earthRadius;
        var offset0 = ToEcef(origin.Latitude, origin.Longitude);
        double latRad = origin.Latitude * Math.PI / 180.0;
        double lonRad = origin.Longitude * Math.PI / 180.0;
        var north0 = new Vec3(
            -Math.Sin(latRad) * Math.Cos(lonRad),
            -Math.Sin(latRad) * Math.Sin(lonRad),
            Math.Cos(latRad));
        var east0 = new Vec3(
            -Math.Sin(lonRad),
            Math.Cos(lonRad),
            0);
        _originData = new()
        {
            North = north0,
            East = east0,
            Offset = offset0
        };
    }

    /// <summary>
    ///     Will match the units used to specify earth radius.
    /// </summary>
    /// <param name="rawPoint"></param>
    /// <returns></returns>
    public PlanarCoordinates ProjectPoint(RawEarthCoordinates rawPoint)
    {
        var offsetDiff = ToEcef(rawPoint.Latitude, rawPoint.Longitude) - _originData.Offset;
        return new((float)(_originData.East * offsetDiff), (float)(_originData.North * offsetDiff));
    }

    public RawEarthCoordinates UnprojectPoint(PlanarCoordinates point)
    {
        var p = _originData.Offset + _originData.East * point.X + _originData.North * point.Y;
        var magnitude = Math.Sqrt(p.X * p.X + p.Y * p.Y + p.Z * p.Z);
        p = p / magnitude;
        return new(
            Math.Asin(p.Z) * 180.0 / Math.PI,
            Math.Atan2(p.Y, p.X) * 180.0 / Math.PI);
    }

    private Vec3 ToEcef(double lat, double lon, bool radians = false)
    {
        if (!radians)
        {
            lat = lat * Math.PI / 180.0;
            lon = lon * Math.PI / 180.0;
        }

        return new(
            EarthRadius * Math.Cos(lat) * Math.Cos(lon),
            EarthRadius * Math.Cos(lat) * Math.Sin(lon),
            EarthRadius * Math.Sin(lat)
        );
    }

    private record OriginData
    {
        public required Vec3 East { get; init; }
        public required Vec3 North { get; init; }
        public required Vec3 Offset { get; init; }
    }

    private record Vec3(double X, double Y, double Z)
    {
        public static Vec3 operator +(Vec3 a, Vec3 b)
        {
            return new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Vec3 operator /(Vec3 a, double scalar)
        {
            return new(a.X / scalar, a.Y / scalar, a.Z / scalar);
        }

        public static Vec3 operator *(Vec3 a, double scalar)
        {
            return new(a.X * scalar, a.Y * scalar, a.Z * scalar);
        }

        public static double operator *(Vec3 a, Vec3 b)
        {
            return (a.X * b.X) + (a.Y * b.Y) + (a.Z * b.Z);
        }

        public static Vec3 operator -(Vec3 a, Vec3 b)
        {
            return new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static Vec3 operator -(Vec3 a)
        {
            return new(-a.X, -a.Y, -a.Z);
        }

        public static Vec3 operator +(Vec3 a)
        {
            return a;
        }
    }
}