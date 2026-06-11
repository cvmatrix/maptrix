namespace CVMatrix.DropOffDefense.SLib.Loc;

public interface ILocRegion : ILocMapElement, ILocTaggable<Tags.IRegionTag>
{
    public IReadOnlyList<ILocCoordinates> Boundary { get; }
    public IReadOnlyList<IReadOnlyList<ILocCoordinates>> SubtractiveBoundaries { get; }
    public IReadOnlySet<ILocMapElement> EncompassedElements { get; }
}