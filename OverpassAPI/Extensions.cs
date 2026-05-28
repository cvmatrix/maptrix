namespace CVMatrix.DropOffDefense.SLib.OverpassAPI;

using Model.Clean;
using Model.Raw;

public static class Extensions
{
    public static CleanData Clean(this RawResponse response) => CleanData.FromRawResponse(response);
}