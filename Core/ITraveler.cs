namespace CVMatrix.DropOffDefense.SLib.Core;

public interface ITraveler
{
    /// <summary>
    /// In speed/sec.
    /// </summary>
    public double Acceleration { get; }
    /// <summary>
    ///  In dist/sec.
    /// </summary>
    public double MaxSpeed { get; }
    public ITravelWay Traveling { get; }
    public Coordinates Position { get; }
    public double DistanceAlongWay { get; }
    public ITravelNode FinalTarget { get; }
}