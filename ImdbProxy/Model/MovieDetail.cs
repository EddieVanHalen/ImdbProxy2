﻿using Newtonsoft.Json;

namespace ImdbProxy.Model;

public class MovieDetail
{
    [JsonProperty("genres")]
    public List<MovieGenre> Genres { get; set; }
    
    [JsonProperty("imdb_id")]
    public string? ImdbId { get; set; }
}