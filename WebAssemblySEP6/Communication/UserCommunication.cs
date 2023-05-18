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

    public class UserCommunication : IUserCommunication
    {
        private string uri = "https://localhost:7044/User";
        private HttpClient httpClient;

        public UserCommunication()
        {
            httpClient = new HttpClient();
        }

        public async Task AddUserAsync(User userToAdd)
        {
            string userToAddAsJson = JsonSerializer.Serialize(userToAdd);

            StringContent content = new StringContent(
                userToAddAsJson,
                Encoding.UTF8,
                "application/json"
            );

            HttpResponseMessage responseMessage = await httpClient.PostAsync(uri, content);
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception($"Error: {responseMessage.StatusCode}, {responseMessage.ReasonPhrase}");
            }
        }

        public async Task<User> GetUserById(int userId)
        {
            HttpResponseMessage responseMessage = await httpClient.GetAsync($"{uri}/{userId}");
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception($"Error: {responseMessage.StatusCode}, {responseMessage.ReasonPhrase}");
            }

            var responseStream = await responseMessage.Content.ReadAsStreamAsync();

            var httpResponse = JsonSerializer.Deserialize<User>(responseStream,
                new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNameCaseInsensitive = true
                });

            return httpResponse;
        }

        public async Task<bool> IsEmailAddressUsed(User user)
        {
            bool isAdressUsed = false;

            HttpResponseMessage responseMessage = await httpClient.GetAsync(uri);
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception($"Error: {responseMessage.StatusCode}, {responseMessage.ReasonPhrase}");
            }

            var responseStream = await responseMessage.Content.ReadAsStreamAsync();

            var httpResponse = JsonSerializer.Deserialize<IList<User>>(responseStream,
                new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNameCaseInsensitive = true
                });

            if (httpResponse != null)
            {
                foreach (var userFromDb in httpResponse)
                {
                    if (userFromDb.EmailAddress.Equals(user.EmailAddress))
                        isAdressUsed = true;
                }

                if (!isAdressUsed)
                {
                    user.UserId = httpResponse.Last().UserId + 1;
                }
            }

            return isAdressUsed;
        }

        public async Task<User> LogIn(User user)
        {
            User userReturned = null; // Initialize as null

            HttpResponseMessage responseMessage = await httpClient.GetAsync(uri + $"/{user.EmailAddress}");
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception($"Error: {responseMessage.StatusCode}, {responseMessage.ReasonPhrase}");
            }

            var responseStream = await responseMessage.Content.ReadAsStreamAsync();

            userReturned = JsonSerializer.Deserialize<User>(responseStream, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            });

            if (userReturned != null && !userReturned.Password.Equals(user.Password))
            {
                userReturned = null; // Set to null if password is incorrect
            }

            return userReturned;
        }
    }
}