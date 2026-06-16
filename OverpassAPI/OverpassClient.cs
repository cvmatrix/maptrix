namespace CVMatrix.Maptrix.OverpassAPI;

using Model.Raw;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Text.Json;

public class OverpassClient : IDisposable
{
    private readonly HttpClient _httpClient = new()
    {
        BaseAddress = new("https://overpass-api.de"),
        DefaultRequestHeaders =
        {
            UserAgent =
            {
                new("Godot", "4.6.3")
            }
        }
    };

    public void Dispose()
    {
        _httpClient.Dispose();
    }

    public async Task<Response?> MakeQuery(string overpassql)
    {
        var body = new FormUrlEncodedContent([new("data", overpassql)]);
        var response = await _httpClient.PostAsync("api/interpreter", body);
        _ = response.EnsureSuccessStatusCode();
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        if (!response.IsSuccessStatusCode) return null;
        var responseString = await response.Content.ReadAsStringAsync();
        return new(responseString, JsonSerializer.Deserialize<RawResponse>(responseString, options)!);
    }

    public Task<Response> MakeQuery(OverpassQuery query)
    {
        return MakeQuery(query.AsQL());
    }

    public record Response(string Json, RawResponse Model);
}