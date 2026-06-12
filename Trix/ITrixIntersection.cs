namespace CVMatrix.Maptrix.Trix;

public interface ITrixIntersection : ITrixMapElement, ITrixTaggable<Tags.IIntersectionTag>
{
    public IReadOnlySet<ITrixWay> Incoming { get; }
    public IReadOnlySet<ITrixWay> Outgoing { get; }
    public ITrixCoordinates Position { get; }
}