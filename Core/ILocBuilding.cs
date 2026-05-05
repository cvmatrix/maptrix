namespace CVMatrix.DropOffDefense.SLib.Core;

public interface ILocBuilding : ILocMapElement, ILocTaggable<Tags.ILocBuildingTag>
{
    public IReadOnlyList<Coordinates> Boundary { get; }
}