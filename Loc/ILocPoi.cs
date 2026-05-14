namespace CVMatrix.DropOffDefense.SLib.Loc;

public interface ILocPoi : ILocMapElement, ILocTaggable<Tags.ILocPoiTag>
{
    public LocCoordinates Position { get; }
}