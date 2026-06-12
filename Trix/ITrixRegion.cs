namespace CVMatrix.Maptrix.Trix;

public interface ITrixRegion : ITrixMapElement, ITrixTaggable<Tags.IRegionTag>
{
    public IReadOnlyList<IReadOnlyList<ITrixCoordinates>> SubtractiveBoundaries { get; }
    public IReadOnlyList<ITrixCoordinates> Boundary { get; }
    public IReadOnlySet<ITrixIntersection> EncompassesIntersections { get; }
    public IReadOnlySet<ITrixPoi> EncompassesPois { get; }
    public IReadOnlySet<ITrixRegion> EncompassesRegions { get; }
    public IReadOnlySet<ITrixWay> EncompassesWays { get; }
}