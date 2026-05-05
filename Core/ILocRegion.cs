namespace CVMatrix.DropOffDefense.SLib.Core;

public interface ILocRegion : ILocMapElement, ILocTaggable<Tags.ILocRegionTag>
{
    public IReadOnlyList<Coordinates> Boundary { get; }
    public IReadOnlySet<ILocMapElement> EncompassedElements { get; }
}