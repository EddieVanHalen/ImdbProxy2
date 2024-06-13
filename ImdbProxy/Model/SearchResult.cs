using Newtonsoft.Json;

namespace ImdbProxy.Model;

public class SearchResult
{
    [JsonProperty("Search")] public List<Movie> Search { get; set; }
    [JsonProperty("Response")] public string? Response { get; set; }
}