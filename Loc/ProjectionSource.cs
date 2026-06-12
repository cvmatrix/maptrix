namespace CVMatrix.Maptrix.Loc;

public record ProjectionSource
{
    public const double EARTH_RADIUS_METERS = 6_371_000.0;
    public const double EARTH_RADIUS_FEET = 20_902_230.0;
    public RawEarthCoordinates Origin { get; }
    public double EarthRadius { get; }
    private readonly OriginData _originData;
    public ProjectionSource(RawEarthCoordinates origin, double earthRadius)
    {
        Origin = origin;
        EarthRadius = earthRadius;
        var offset0 = ToEcef(origin.Latitude, origin.Latitude);
        var north0 = new Vec3(
            -Math.Sin(origin.Latitude) * Math.Cos(origin.Latitude),
            -Math.Sin(origin.Latitude) * Math.Sin(origin.Latitude),
            Math.Cos(origin.Latitude));
        var east0 = new Vec3(
            -Math.Sin(origin.Latitude),
            Math.Cos(origin.Latitude),
            0);
        _originData = new()
        {
            North = north0,
            East = east0,
            Offset = offset0,
        };
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

    /// <summary>
    /// Will match the units used to specify earth radius.
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
        var p = _originData.Offset + (_originData.East * point.X) + (_originData.North * point.Y);
        var magnitude = Math.Sqrt((p.X * p.X) + (p.Y * p.Y) + (p.Z * p.Z));
        p = p / magnitude;
        return new(
            Math.Asin(p.Z) * 180.0 / Math.PI,
            Math.Atan2(p.Y, p.X) * 180.0 / Math.PI);
    }

    private record Vec3(double X, double Y, double Z)
    {
        public static Vec3 operator +(Vec3 a) => a;
        public static Vec3 operator -(Vec3 a) => new(-a.X, -a.Y, -a.Z);
        public static Vec3 operator +(Vec3 a, Vec3 b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        public static Vec3 operator -(Vec3 a, Vec3 b) => new(a.X - b.X, a.Y - b.Y, a.Z - a.Z);
        public static Vec3 operator *(Vec3 a, double scalar) => new(a.X * scalar, a.Y * scalar, a.Z * scalar);
        public static Vec3 operator /(Vec3 a, double scalar) => new(a.X / scalar, a.Y / scalar, a.Z / scalar);
        public static double operator *(Vec3 a, Vec3 b) => (a.X * b.X) + (a.Y * b.Y) + (a.Z * b.Z);
    }

    private record OriginData
    {
        public required Vec3 North { get; init; }
        public required Vec3 East { get; init; }
        public required Vec3 Offset { get; init; }
    }
}