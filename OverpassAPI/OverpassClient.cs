namespace CVMatrix.DropOffDefense.SLib.OverpassAPI;

using Model.Raw;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Text.Json;
public class OverpassClient : IDisposable
{
    private readonly HttpClient _httpClient = new HttpClient()
    {
        BaseAddress = new Uri("https://overpass-api.de")
    };

    public async Task<(string ResponseJsonText, RawResponse ResponseModel)> MakeQuery(string overpassql)
    {
        var body = new FormUrlEncodedContent([new("data", overpassql)]);

        var response = await _httpClient.PostAsync("api/interpreter", body);
        _ = response.EnsureSuccessStatusCode();
        var options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
        };
        var responseString = await response.Content.ReadAsStringAsync();
        return (responseString, JsonSerializer.Deserialize<RawResponse>(responseString, options)!);
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }
}