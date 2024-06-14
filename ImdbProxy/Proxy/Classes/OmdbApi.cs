using System.Net.Http;
using ImdbProxy.Model;
using ImdbProxy.Proxy.Interfaces;
using Newtonsoft.Json;

namespace ImdbProxy.Proxy.Classes;

public class OmdbApi : IMovieFinder
{
    private string _api = "57f5317f";

    public async Task<Movie?> RequestMovieAsync(string movieId)
    {
        throw new Exception();
        using (var client = new HttpClient())
        {
            var url = new Uri($"http://www.omdbapi.com/?i={movieId}&apikey={_api}");

            var response = await client.GetStringAsync(url);

            var movie = JsonConvert.DeserializeObject<Movie>(response);

            if (movie?.Response == "True")
            {
                return movie;
            }
        }

        return null;
    }


    public async Task<IList<Movie>> RequestMoviesAsync(string movieName)
    {
        throw new Exception();
        var moviesList = new List<Movie>();

        using (var client = new HttpClient())
        {
            var url = new Uri($"http://www.omdbapi.com/?s={movieName}&page=1&apikey={_api}");

            var response = await client.GetStringAsync(url);

            var movies = JsonConvert.DeserializeObject<SearchResult>(response);

            if (movies?.Response == "True")
            {
                foreach (var item in movies.Search)
                {
                    moviesList.Add(item);
                }

                return moviesList;
            }
        }

        return null;
    }
}