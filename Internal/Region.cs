namespace CVMatrix.DropOffDefense.SLib.Internal;

using Loc;
using Loc.Tags;
using Util.RegionManagement;

internal class Region : TaggableMapElement<IRegionTag>, ILocRegion
{
    public required IReadOnlyList<Coordinates> Boundary { get; set; }
    public required IReadOnlyList<IReadOnlyList<Coordinates>> SubtractiveBoundaries { get; set; }
    IReadOnlyList<ILocCoordinates> ILocRegion.Boundary => Boundary;

    IReadOnlyList<IReadOnlyList<ILocCoordinates>> ILocRegion.SubtractiveBoundaries => SubtractiveBoundaries;
    IReadOnlySet<ILocMapElement> ILocRegion.EncompassedElements => throw new NotImplementedException();

    protected override IElementHandle<Region> GetRegionElementHandle(RegionManager<Region, Way, IPointElement> manager)
    {
        return manager.GetRegion(this);
    }

    protected override IRegionTag? SerializeTag(string key, string value)
    {
        return null;
    }
}