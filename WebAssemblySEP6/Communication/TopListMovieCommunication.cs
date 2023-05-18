using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Model;
using WebAssemblySEP6.Model;

namespace WebAssemblySEP6.Communication
{
    public class TopListMovieCommunication : ITopListMovieCommunication
    {
        
        private string uri = "https://localhost:7044/TopListMovie";
        private HttpClient httpClient;

        public TopListMovieCommunication()
        {
            httpClient = new HttpClient();
        }
        
        
        public async Task AddTopListMoviesAsyncList(TopList topList, List<Movie> movies)
        {
            var data = new
            {
                TopList = topList,
                Movies = movies
            };
            
            string jsonData = JsonSerializer.Serialize(data);
            
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            
            HttpResponseMessage responseMessage = await httpClient.PostAsync(uri, content);
            
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception($"Error: {responseMessage.StatusCode}, {responseMessage.ReasonPhrase}");
            }
        }
        
        public async Task DeleteTopListMovieById(int id)
        {
            string requestUri = $"{uri}/{id}";

            HttpResponseMessage responseMessage = await httpClient.DeleteAsync(requestUri);
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception($"Error: {responseMessage.StatusCode}, {responseMessage.ReasonPhrase}");
            }
        }
    }
}