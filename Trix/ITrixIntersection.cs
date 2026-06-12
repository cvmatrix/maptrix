namespace CVMatrix.Maptrix.Trix;

public interface ITrixIntersection : ITrixMapElement, ITrixTaggable<Tags.IIntersectionTag>
{
    public ITrixCoordinates Position { get; }
    public IReadOnlySet<ITrixWay> Incoming { get; }
    public IReadOnlySet<ITrixWay> Outgoing { get; }
}