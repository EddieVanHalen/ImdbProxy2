using Newtonsoft.Json;

namespace ImdbProxy.Model;

public class Movie
{
    [JsonProperty("imdbID")] public string? Id { get; set; }
    
    [JsonProperty("Title")] public string? Title { get; set; }

    [JsonProperty("Year")] public string? ReleaseDate { get; set; }

    [JsonProperty("Actors")] public string? Actors { get; set; }
    
    [JsonProperty("Genre")] public string? Genre { get; set; }

    [JsonProperty("Plot")] public string? Overview { get; set; }

    [JsonProperty("Poster")] public string? Poster { get; set; }

    [JsonProperty("imdbRating")] public string? Rating { get; set; }

    [JsonProperty("Response")] public string? Response { get; set; }

    public override string ToString()
    {
        return Title;
    }

    public Movie()
    {
        
    }
    
    public Movie(TmdbMovie tmdbMovie)
    {
        Id = tmdbMovie.Id;
        Title = tmdbMovie.Title;
        ReleaseDate = tmdbMovie.ReleaseDate;
        Actors = tmdbMovie.Actors;
        Overview = tmdbMovie.Overview;
        Poster = tmdbMovie.Poster;
        Rating = tmdbMovie.Rating;
        Genre = tmdbMovie.Genres;
    }
}