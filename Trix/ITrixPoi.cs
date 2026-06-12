namespace CVMatrix.Maptrix.Trix;

public interface ITrixPoi : ITrixMapElement, ITrixTaggable<Tags.IPoiTag>
{
    public ITrixCoordinates Position { get; }
}