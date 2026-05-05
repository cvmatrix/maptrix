namespace CVMatrix.DropOffDefense.SLib.Core;

public interface ILocBuilding
{
    public IReadOnlyList<Coordinates> Boundary { get; }
}