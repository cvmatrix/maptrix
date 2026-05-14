namespace CVMatrix.DropOffDefense.SLib.Loc;

public interface ILocBuilding : ILocMapElement, ILocTaggable<Tags.ILocBuildingTag>
{
    public IReadOnlyList<LocCoordinates> Boundary { get; }
}