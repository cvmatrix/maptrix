namespace CVMatrix.DropOffDefense.SLib.Internal;

using Loc;
using Loc.Tags;

internal class Region : TaggableMapElement<IRegionTag>, ILocRegion
{
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