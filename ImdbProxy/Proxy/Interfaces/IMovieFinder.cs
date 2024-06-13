using ImdbProxy.Model;

namespace ImdbProxy.Proxy.Interfaces;

public interface IMovieFinder
{
    Task<Movie?> RequestMovieAsync(string movieId);

    Task<ICollection<Movie>> RequestMoviesAsync(string movieName);
}