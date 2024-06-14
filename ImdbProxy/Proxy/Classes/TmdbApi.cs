using System.Net.Http;
using ImdbProxy.Model;
using ImdbProxy.Proxy.Interfaces;
using Newtonsoft.Json;

namespace ImdbProxy.Proxy.Classes;

public class TmdbApi : IMovieFinder
{
    private string _apiKey = "b15ead681f57a1f60e3b4f04f16c70aa";
    
    public async Task<Movie?> RequestMovieAsync(string movieId)
    {
        using var _httpClient = new HttpClient(); 
        
        var url = $"https://api.themoviedb.org/3/movie/{movieId}?api_key={_apiKey}";
        var response = await _httpClient.GetStringAsync(url);
        var movie = JsonConvert.DeserializeObject<TmdbMovie>(response);

        if (movie != null)
        {
            movie.Actors = await GetMovieCastAsync(movieId);
            movie.Poster = GetPosterUrl(movie.PosterPath);
            movie.Genres = await GetGenresAsync(movieId);

            return new Movie(movie);
        }

        return null;
    }
    
    public async Task<IList<Movie>> RequestMoviesAsync(string query)
    {
        using var _httpClient = new HttpClient();
        
        var url = $"https://api.themoviedb.org/3/search/movie?api_key={_apiKey}&query={query}";
        var response = await _httpClient.GetStringAsync(url);
        var searchResult = JsonConvert.DeserializeObject<TmdbSearchResult>(response);
        var movies = searchResult?.Results ?? new List<TmdbMovie>();
        IList<Movie> moviesToSend = new List<Movie>();
        
        foreach (var movie in movies)
        {
            moviesToSend.Add(new Movie(movie));
        }
        
        return moviesToSend;
    }

    private string GetPosterUrl(string posterPath)
    {
        return $"https://image.tmdb.org/t/p/w500{posterPath}";
    }
    
    private async Task<string> GetGenresAsync(string movieId)
    {
        using var _httpClient = new HttpClient();
        var url = $"https://api.themoviedb.org/3/movie/{movieId}?api_key={_apiKey}&append_to_response=genres";
        var response = await _httpClient.GetStringAsync(url);
        var movieDetail = JsonConvert.DeserializeObject<MovieDetail>(response);
        return movieDetail != null ? string.Join(", ", movieDetail.Genres.Select(g => g.Name)) : "N/A";
    }

    private async Task<string> GetMovieCastAsync(string movieId)
    {
        using var _httpClient = new HttpClient();
        var url = $"https://api.themoviedb.org/3/movie/{movieId}/credits?api_key={_apiKey}";
        var response = await _httpClient.GetStringAsync(url);
        var credits = JsonConvert.DeserializeObject<Credits>(response);
        return credits != null ? string.Join(", ", credits.Cast.Select(c => c.Name)) : "N/A";
    }
}