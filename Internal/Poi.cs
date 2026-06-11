namespace CVMatrix.DropOffDefense.SLib.Internal;

using Loc;
using Loc.Tags;
using Util.RegionManagement;

internal class Poi : TaggableMapElement<IPoiTag>, ILocPoi, IPointElement
{
    public required Coordinates Position { get; set; }

    ILocCoordinates ILocPoi.Position => Position;

    protected override IElementHandle<Region> GetRegionElementHandle(RegionManager<Region, Way, IPointElement> manager)
    {
        return manager.GetPoint(this);
    }

    protected override IPoiTag? SerializeTag(string key, string value)
    {
        return null;
    }
}