namespace CVMatrix.Maptrix.Internal;

using Trix;
using Trix.Tags;
using Util.RegionManagement;

internal class Poi : TaggableMapElement<IPoiTag>, ITrixPoi, IPointElement
{
    public required Coordinates Position { get; set; }

    ITrixCoordinates ITrixPoi.Position => Position;

    protected override IElementHandle<Region> GetRegionElementHandle(RegionManager<Region, Way, IPointElement> manager)
    {
        return manager.GetPoint(this);
    }

    protected override IPoiTag? SerializeTag(string key, string value)
    {
        return null;
    }
}