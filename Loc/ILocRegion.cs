namespace CVMatrix.DropOffDefense.SLib.Loc;

public interface ILocRegion : ILocMapElement, ILocTaggable<Tags.IRegionTag>
{
    public IReadOnlyList<ILocCoordinates> Boundary { get; }
    public IReadOnlyList<ILocCoordinates>? SubtractiveBoundary { get; }
    public IReadOnlySet<ILocMapElement> EncompassedElements { get; }
}