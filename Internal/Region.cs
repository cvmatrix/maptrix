namespace CVMatrix.DropOffDefense.SLib.Internal;

using Loc;
using Loc.Tags;
using Util.RegionManagement;

internal class Region : TaggableMapElement<IRegionTag>, ILocRegion
{
    protected override IElementHandle<Region> GetRegionElementHandle(RegionManager<Region, Way, IPointElement> manager) => manager.GetRegion(this);

    protected override IRegionTag? SerializeTag(string key, string value)
    {
        throw new NotImplementedException();
    }

    public required IReadOnlyList<Coordinates> Boundary { get; set; }
    public IReadOnlyList<Coordinates>? SubtractiveBoundary { get; set; }

    IReadOnlyList<ILocCoordinates>? ILocRegion.SubtractiveBoundary => SubtractiveBoundary;
    IReadOnlySet<ILocMapElement> ILocRegion.EncompassedElements => throw new NotImplementedException();
    IReadOnlyList<ILocCoordinates> ILocRegion.Boundary => Boundary;
}