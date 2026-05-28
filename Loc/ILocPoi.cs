namespace CVMatrix.DropOffDefense.SLib.Loc;

public interface ILocPoi : ILocMapElement, ILocTaggable<Tags.IPoiTag>
{
    public ILocCoordinates Position { get; }
}