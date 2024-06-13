using System.Runtime.Caching;
using System.Windows;
using ImdbProxy.Model;
using ImdbProxy.Proxy.Interfaces;

namespace ImdbProxy.Proxy.Classes;

public class Proxy : IMovieFinder
{
    private MemoryCache _cache;
    
    private readonly IMovieFinder _omdb;
    private readonly IMovieFinder _tmdb;

    public Proxy()
    {
        _omdb = new OmdbApi();
        _tmdb = new TmdbApi();
        _cache = MemoryCache.Default;
    }
    
    public async Task<Movie?> RequestMovieAsync(string movieId)
    {
        try
        {
            var movie = GetMovieFromCache(movieId);

            if (movie is not null)
            {
                return movie;
            }
            
            movie = await RequestMovieAsync(movieId);
            
            if (movie is not null)
            {
               AddMovieToCache(movie.Id!, movie);
            }
            
            return movie;
        }
        catch (Exception e)
        {
            return await _tmdb.RequestMovieAsync(movieId);
        }
    }
    
    public Task<ICollection<Movie>> RequestMoviesAsync(string movieName)
    {
        try
        {
            return _omdb.RequestMoviesAsync(movieName);
        }
        catch (Exception e)
        {
            return _tmdb.RequestMoviesAsync(movieName);
        }
    }
    
    private Movie? GetMovieFromCache(string movieId)
    {
        if (_cache.Contains(movieId))
        {
            var item = _cache.GetCacheItem(movieId);
            
            if (item is not null)
            {
                return (item.Value as Movie)!;
            }
        }

        return null;
    }
    
    private void AddMovieToCache(string movieId, Movie movie)
    {
        var policy = new CacheItemPolicy
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(10)
        };
        
        _cache.Add(movieId, movie, policy);
    }
}