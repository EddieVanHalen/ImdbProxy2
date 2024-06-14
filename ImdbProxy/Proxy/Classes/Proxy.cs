using System.Runtime.Caching;
using ImdbProxy.Model;
using ImdbProxy.Proxy.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ImdbProxy.Proxy.Classes;

public class Proxy : IMovieFinder
{
    private MemoryCache _cache;
    
    private readonly IMovieFinder _omdb;
    private readonly IMovieFinder _tmdb;

    public Proxy()
    {
        _omdb = App.Provider.GetService<OmdbApi>()!;
        _tmdb = App.Provider.GetService<TmdbApi>()!;
        _cache = MemoryCache.Default;
    }
    
    public async Task<Movie?> RequestMovieAsync(string movieId)
    {
        var movie = GetMovieFromCache(movieId);

        if (movie is not null)
        {
            return movie;
        }
        
        try
        {
            movie = await _omdb.RequestMovieAsync(movieId);
            
            if (movie is not null)
            {
               AddMovieToCache(movie.Id!, movie);
            }
            
            return movie;
        }
        catch (Exception e)
        {
            movie = await _tmdb.RequestMovieAsync(movieId);
            
            if (movie is not null)
            {
                AddMovieToCache(movie.Id!, movie);
            }
            
            return movie;
        }
    }
    
    public async Task<IList<Movie>> RequestMoviesAsync(string movieName)
    {
        try
        {
            return await _omdb.RequestMoviesAsync(movieName);
        }
        catch (Exception e)
        {
            return await _tmdb.RequestMoviesAsync(movieName);
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