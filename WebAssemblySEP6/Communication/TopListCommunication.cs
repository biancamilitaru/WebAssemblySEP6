using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Model;
using WebAssemblySEP6.Model;

namespace WebAssemblySEP6.Communication
{
    public class TopListCommunication : ITopListCommunication
    {
        private string uri = "https://localhost:7044/TopList";
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

                return isIdUsed;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false; // Exception occurred, ID status is unknown
            }

        }
    }
}