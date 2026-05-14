namespace CVMatrix.DropOffDefense.SLib.Loc;

using OverpassAPI;
using OverpassAPI.Model;

public class LocMap
{
    public RawEarthCoordinates ProjectionOrigin { get; }

    private LocMap()
    {

    }

    public static LocMap FromOverpassData(OverpassAPI.Model.Clean.CleanData data, RawEarthCoordinates projectionOrigin)
    {
        throw new NotImplementedException();
    }
}