namespace CVMatrix.Maptrix.Trix;

public interface ITrixWay : ITrixMapElement, ITrixTaggable<Tags.IWayTag>
{
    public float PathLength { get; }
    public IReadOnlyList<ITrixCoordinates> Path { get; }
    public ITrixIntersection From { get; }
    public ITrixIntersection To { get; }
    public ITrixWay? AdjacentReverse { get; }
}