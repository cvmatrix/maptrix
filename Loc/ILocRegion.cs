namespace CVMatrix.DropOffDefense.SLib.Loc;

public interface ILocRegion : ILocMapElement, ILocTaggable<Tags.IRegionTag>
{
    public IReadOnlyList<ILocCoordinates> Boundary { get; }
    public IReadOnlyList<IReadOnlyList<ILocCoordinates>> SubtractiveBoundaries { get; }
    public IReadOnlySet<ILocPoi> EncompassesPois { get; }
    public IReadOnlySet<ILocRegion> EncompassesRegions { get; }
    public IReadOnlySet<ILocWay> EncompassesWays { get; }
    public IReadOnlySet<ILocIntersection> EncompassesIntersections { get; }
}