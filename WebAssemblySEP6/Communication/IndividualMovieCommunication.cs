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

public class IndividualMovieCommunication : IIndividualMovieCommunication
{
    private HttpClient httpClient;

    public IndividualMovieCommunication()
    {
        httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJjZjE2NTgwMTBmM2I3YzJhZTEyMDg4ZjYwYjA2OTk4NyIsInN1YiI6IjY0NWI4YTUxMWI3MGFlMDE0NWVlZmNiMyIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.qcTThPaLPgjcTtIC0ECwH2lcLBTGugvE4yEXGEOspDc");
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<Movie> GetMovieAsync(int Id)
    {
        var movieDetails = await GetMovieDetails(Id);
        var movieCredits = await GetMovieCreditsAsync(Id);
        var movie = new Movie
        {
            Title = movieDetails.Title,
            Description = movieDetails.Description,
            Directors = new List<string>(),
            Image = movieDetails.Image,
            AvgRating = movieDetails.AvgRating,
            NumberOfVotes = movieDetails.NumberOfVotes,
            Actors = new List<string>()
        };
        
        foreach (var crew in movieCredits.crewMembers)
        {
            if (crew.Job.Contains("Director"))
            {
                movie.Directors.Add(crew.Name);
            }
        }

        foreach (var cast in movieCredits.castMembers)
        {
            movie.Actors.Add(cast.Name);
        }

        return movie;
    }

    private async Task<HttpResponseMovieDetails> GetMovieDetails(int Id)
    {
        var responseMessage = await httpClient.GetAsync($"https://api.themoviedb.org/3/movie/{Id}");
        
        if (!responseMessage.IsSuccessStatusCode)
        { 
            throw new Exception($"Error: '{responseMessage.StatusCode}' - {await responseMessage.Content.ReadAsStringAsync()} - when calling GetMovieAsync");
        }
      
        var responseStream = await responseMessage.Content.ReadAsStreamAsync();
        
        var httpResponse = JsonSerializer.Deserialize<HttpResponseMovieDetails>(responseStream, new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase, 
            PropertyNameCaseInsensitive = true
        });

        return httpResponse;
    }

    private async Task<HttpResponseCredits> GetMovieCreditsAsync(int Id)
    {
        var responseMessage = await httpClient.GetAsync($"https://api.themoviedb.org/3/movie/{Id}/credits");
        
        if (!responseMessage.IsSuccessStatusCode)
        { 
            throw new Exception($"Error: '{responseMessage.StatusCode}' - {await responseMessage.Content.ReadAsStringAsync()} - when calling GetMovieCreditsAsync");
        }
      
        var responseStream = await responseMessage.Content.ReadAsStreamAsync();
        
        var httpResponse = JsonSerializer.Deserialize<HttpResponseCredits>(responseStream, new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase, 
            PropertyNameCaseInsensitive = true
        });

        return httpResponse;
    }
    
}