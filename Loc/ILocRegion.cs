namespace CVMatrix.DropOffDefense.SLib.Loc;

public interface ILocRegion : ILocMapElement, ILocTaggable<Tags.ILocRegionTag>
{
    public IReadOnlyList<LocCoordinates> Boundary { get; }
    public IReadOnlySet<ILocMapElement> EncompassedElements { get; }
}