using Newtonsoft.Json;

namespace ImdbProxy.Model;

public class TmdbMovie
{
    [JsonProperty("id")] public string Id { get; set; }
    
    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("release_date")]
    public string ReleaseDate { get; set; }

    [JsonProperty("poster_path")]
    public string PosterPath { get; set; }

    [JsonProperty("overview")]
    public string Overview { get; set; }

    [JsonProperty("vote_average")]
    public string Rating { get; set; }
    
    [JsonIgnore]
    public string Genres { get; set; }

    [JsonIgnore]
    public string Poster { get; set; }

    [JsonIgnore]
    public string Actors { get; set; }
}