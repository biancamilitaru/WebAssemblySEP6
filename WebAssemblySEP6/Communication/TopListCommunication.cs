using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Model;

namespace WebAssemblySEP6.Communication
{
    public class TopListCommunication : ITopListCommunication
    {
        // TODO - update this links with the good ones after deploying the web online
        private string uri = "https://backend4hell.azurewebsites.net/api/TopList";
        private HttpClient httpClient;

        public TopListCommunication()
        {
            httpClient = new HttpClient();
        }
        
        public async Task AddTopListAsync(TopList topList)
        {
            string topListAsJson = JsonSerializer.Serialize(topList);
            
            Console.WriteLine("TopListCommunication - AddTopListAsync"+topListAsJson);

            StringContent content = new StringContent(
                topListAsJson, Encoding.UTF8, "application/json");

            HttpResponseMessage responseMessage = await httpClient.PostAsync(uri, content);
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception($"Error: {responseMessage.StatusCode}, {responseMessage.ReasonPhrase}");
            }
        }

        public async Task<bool> IsIdCorrect(TopList topList)
        {
            Console.WriteLine("In the TopListCommunication class in the IsIdCorrect method");
            try
            {
                HttpResponseMessage responseMessage = await httpClient.GetAsync(uri);
                if (!responseMessage.IsSuccessStatusCode)
                {
                    return false; // Request failed, ID status is unknown
                }

                var responseStream = await responseMessage.Content.ReadAsStreamAsync();
                var topLists = await JsonSerializer.DeserializeAsync<IList<TopList>>(responseStream, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNameCaseInsensitive = true
                });

                bool isIdUsed = topLists.Any(tl => tl.Id == topList.Id);
                if (isIdUsed)
                {
                    topList.Id = topLists.Max(tl => tl.Id) + 1;
                }

                Console.WriteLine(topList.Id + " "+isIdUsed );
                return isIdUsed;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false; // Exception occurred, ID status is unknown
            }
        }

        public async Task DeleteTopListById(int id)
        {
            try
            {
                string requestUri = $"{uri}/{id}";

                HttpResponseMessage responseMessage = await httpClient.DeleteAsync(requestUri);
                if (!responseMessage.IsSuccessStatusCode)
                {
                    throw new Exception($"Error deleting TopList with ID {id}: {responseMessage.StatusCode}, {responseMessage.ReasonPhrase}");
                }

                // Optionally, perform any additional actions after successful deletion
                Console.WriteLine($"TopList with ID {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting TopList with ID {id}: {ex.Message}");
                // Handle any exceptions that occur during deletion
            }
        }

        public async Task<IList<TopList>> GetAllTopListsByIdAsync(int userId)
        {
            string requestUri = $"{uri}/check-id/{userId}";

            HttpResponseMessage responseMessage = await httpClient.GetAsync(requestUri);
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception($"Error: {responseMessage.StatusCode}, {responseMessage.ReasonPhrase}");
            }

            var responseStream = await responseMessage.Content.ReadAsStreamAsync();
            var topLists = await JsonSerializer.DeserializeAsync<IList<TopList>>(responseStream,
                new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNameCaseInsensitive = true
                });

            return topLists;
        }
        
    }
}