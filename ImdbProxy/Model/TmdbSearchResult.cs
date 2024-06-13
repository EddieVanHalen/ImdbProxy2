using Newtonsoft.Json;

namespace ImdbProxy.Model;

public class TmdbSearchResult
{
    [JsonProperty("results")] public List<TmdbMovie> Results { get; set; }
}