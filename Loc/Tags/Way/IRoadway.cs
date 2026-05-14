namespace CVMatrix.DropOffDefense.SLib.Loc.Tags.Way;

public interface IRoadway : ILocWayTag
{
    public string RawTag { get; }
    public int? LaneCount { get; }
    public int? SpeedLimit { get; }

}