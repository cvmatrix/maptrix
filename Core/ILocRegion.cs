namespace CVMatrix.DropOffDefense.SLib.Core;

public interface ILocRegion
{
    public IReadOnlyList<Coordinates> Boundary { get; }
    public IReadOnlySet<ILocMapElement> EncompassedElements { get; }
}