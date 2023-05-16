using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Model;
using WebAssemblySEP6.Communication;

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
        
        Console.WriteLine(userToAddAsJson);

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

    public async Task<bool> IsEmailAddressUsed(User user)
    {
        bool isAdressUsed = false;
        string emailToJson = JsonSerializer.Serialize(user.EmailAddress);

        StringContent content = new StringContent(
            emailToJson,
            Encoding.UTF8,
            "application/json"
        );

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
}