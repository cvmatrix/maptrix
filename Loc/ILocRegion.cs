namespace CVMatrix.DropOffDefense.SLib.Loc;

public interface ILocRegion : ILocMapElement, ILocTaggable<Tags.IRegionTag>
{
    public IReadOnlyList<LocCoordinates> Boundary { get; }
    public IReadOnlyList<LocCoordinates>? SubtractiveBoundary { get; }
    public IReadOnlySet<ILocMapElement> EncompassedElements { get; }
}