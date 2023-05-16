using System.Net.Http.Headers;
using System.Text.Json;
using Model.HttpResponse;
using Model;

namespace WebAssemblySEP6.Communication
{
    public class IndividualMovieCommunication : IIndividualMovieCommunication
    {
        private HttpClient httpClient;

        public IndividualMovieCommunication()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJjZjE2NTgwMTBmM2I3YzJhZTEyMDg4ZjYwYjA2OTk4NyIsInN1YiI6IjY0NWI4YTUxMWI3MGFlMDE0NWVlZmNiMyIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.qcTThPaLPgjcTtIC0ECwH2lcLBTGugvE4yEXGEOspDc");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var movieDetails = await GetMovieDetails(id);
            var movieCredits = await GetMovieCreditsAsync(id);

            var movie = new Movie
            {
                Id = movieDetails.Id,
                Title = movieDetails.Title,
                Description = movieDetails.Description,
                Directors = new List<string>(),
                Image = $"http://image.tmdb.org/t/p/w500/{movieDetails.Image}",
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

        public async Task<IList<int>> GetMoviesBySearchName(string name)
        {
            var movies = await GetMovieSearch(name);
            IList<int> movieToReturn = new List<int>();

            for (int i = 0; i < 10 && i < movies.results.Count; i++)
            {
                movieToReturn.Add(movies.results[i].Id);
            }

            return movieToReturn;
        }

        private async Task<HttpResponseMovieDetails> GetMovieDetails(int id)
        {
            var responseMessage = await httpClient.GetAsync($"https://api.themoviedb.org/3/movie/{id}");

            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception(
                    $"Error: '{responseMessage.StatusCode}' - {await responseMessage.Content.ReadAsStringAsync()} - when calling GetMovieDetails");
            }

            var responseStream = await responseMessage.Content.ReadAsStreamAsync();

            var httpResponse = JsonSerializer.Deserialize<HttpResponseMovieDetails>(responseStream,
                new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNameCaseInsensitive = true
                });
            
            return httpResponse; 
        }
    

        private async Task<HttpResponseCredits> GetMovieCreditsAsync(int id)
        {
            var responseMessage = await httpClient.GetAsync($"https://api.themoviedb.org/3/movie/{id}/credits");

            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception(
                    $"Error: '{responseMessage.StatusCode}' - {await responseMessage.Content.ReadAsStringAsync()} - when calling GetMovieCreditsAsync");
            }

            var responseStream = await responseMessage.Content.ReadAsStreamAsync();

            var httpResponse = JsonSerializer.Deserialize<HttpResponseCredits>(responseStream,
                new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNameCaseInsensitive = true
                });

            return httpResponse;
        }

        private async Task<HttpResponseSearchMovie> GetMovieSearch(string name)
        {
            try
            {
                var responseMessage = await httpClient.GetAsync($"https://api.themoviedb.org/3/search/movie?query={name}");

                if (!responseMessage.IsSuccessStatusCode)
                {
                    throw new Exception(
                        $"Error: '{responseMessage.StatusCode}' - {await responseMessage.Content.ReadAsStringAsync()} - when calling GetMovieSearchByName");
                }

                var responseStream = await responseMessage.Content.ReadAsStreamAsync();

                var httpResponse = JsonSerializer.Deserialize<HttpResponseSearchMovie>(responseStream,
                    new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                        PropertyNameCaseInsensitive = true
                    });

                return httpResponse;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
           
        }

    }
}