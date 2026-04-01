namespace CVMatrix.DropOffDefense.SLib.Core.Handles;

public interface ITravelerHandle : IHandle<Messages.ETravelerMessage>
{
    /// <summary>
    /// In speed/sec.
    /// </summary>
    public double Acceleration { get; }
    /// <summary>
    ///  In dist/sec.
    /// </summary>
    public double MaxSpeed { get; }
    public ITravelWayHandle Traveling { get; }
    public Coordinates Position { get; }
    public double DistanceAlongWay { get; }
    public ITravelNodeHandle FinalTarget { get; }
}