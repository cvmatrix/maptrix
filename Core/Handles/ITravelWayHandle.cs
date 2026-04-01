namespace CVMatrix.DropOffDefense.SLib.Core.Handles;

public interface ITravelWayHandle : IHandle<Messages.ETravelWayMessage, Stats.TravelWayStats>
{
    public double Distance { get; }
    public IReadOnlyList<Coordinates> Path { get; }

    /// <summary>
    ///     Should be in order of distance travelled (least first).
    /// </summary>
    public IReadOnlyList<ITravelerHandle> Travelers { get; }
    public ITravelNodeHandle From { get; }
    public ITravelNodeHandle To { get; }
    public bool IsImpeded { get; }
}