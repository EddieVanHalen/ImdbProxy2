using System.Net;
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
        using (var client = new HttpClient())
        {
            var url = new Uri($"http://www.omdbapi.com/?i={movieId}&apikey={_api}");

            try
            {
                var response = await client.GetStringAsync(url);

                var movie = JsonConvert.DeserializeObject<Movie>(response);

                if (movie?.Response == "True")
                {
                    return movie;
                }
            }
            catch (WebException e)
            {
            }
        }

        return null;
    }


    public async Task<ICollection<Movie>> RequestMoviesAsync(string movieName)
    {
        var moviesList = new List<Movie>();

        using (var client = new HttpClient())
        {
            var url = new Uri($"http://www.omdbapi.com/?s={movieName}&apikey={_api}");

            try
            {
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
            catch (WebException e)
            {
            }
        }

        return null;
    }
}