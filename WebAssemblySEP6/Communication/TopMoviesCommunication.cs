using WebAssemblySEP6.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebAssemblySEP6.Model.HttpResponse;

namespace WebAssemblySEP6.Communication;

public class TopMovieCommunication: ITopMoviesCommunication
{
       private HttpClient httpClient;

       public TopMovieCommunication()
    {
        httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJjZjE2NTgwMTBmM2I3YzJhZTEyMDg4ZjYwYjA2OTk4NyIsInN1YiI6IjY0NWI4YTUxMWI3MGFlMDE0NWVlZmNiMyIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.qcTThPaLPgjcTtIC0ECwH2lcLBTGugvE4yEXGEOspDc");
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<IList<Movie>> GetTopMoviesAsync()
    {

        var topMovieDetails = await GetTopMovies();

        var movieList = new List<Movie>();
        foreach (var movieTopList in topMovieDetails.TopMovies)
        {
            var movie = new Movie{
                Title = movieTopList.Title,
                Description = movieTopList.Description,
                Image = movieTopList.Image,
            };
            movieList.Add(movie);
        }

        return movieList;
    }

    private async Task<HttpResponseTopMovies> GetTopMovies()
    {
        var responseMessage = await httpClient.GetAsync($"https://api.themoviedb.org/3/discover/movie?include_adult=false&include_video=false&language=en-US&page=1&sort_by=vote_average.desc&without_genres=99,10755&vote_count.gte=200");
        
        if (!responseMessage.IsSuccessStatusCode)
        { 
            throw new Exception($"Error: '{responseMessage.StatusCode}' - {await responseMessage.Content.ReadAsStringAsync()} - when calling GetMovieAsync");
        }
      
        var responseStream = await responseMessage.Content.ReadAsStreamAsync();
        
        var httpResponse = JsonSerializer.Deserialize<HttpResponseTopMovies>(responseStream, new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase, 
            PropertyNameCaseInsensitive = true
        });

        

        return httpResponse;
    }
}