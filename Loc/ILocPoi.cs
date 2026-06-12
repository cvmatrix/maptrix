namespace CVMatrix.Maptrix.Loc;

public interface ILocPoi : ILocMapElement, ILocTaggable<Tags.IPoiTag>
{
    public ILocCoordinates Position { get; }
}