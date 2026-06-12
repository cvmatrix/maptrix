namespace CVMatrix.Maptrix.OverpassAPI;

using Model.Raw;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Text.Json;
public class OverpassClient : IDisposable
{
    private readonly HttpClient _httpClient = new()
    {
        BaseAddress = new Uri("https://overpass-api.de")
    };

    public async Task<Response> MakeQuery(string overpassql)
    {
        var body = new FormUrlEncodedContent([new("data", overpassql)]);

        var response = await _httpClient.PostAsync("api/interpreter", body);
        _ = response.EnsureSuccessStatusCode();
        var options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
        };
        var responseString = await response.Content.ReadAsStringAsync();
        return new(responseString, JsonSerializer.Deserialize<RawResponse>(responseString, options)!);
    }

    public Task<Response> MakeQuery(OverpassQuery query) => MakeQuery(query.AsQL());
    public void Dispose()
    {
        _httpClient.Dispose();
    }

    public record Response(string Json, RawResponse Model);
}