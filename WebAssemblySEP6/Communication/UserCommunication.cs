﻿using System.Text;
using System.Text.Json;
using Model;

namespace WebAssemblySEP6.Communication;

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

    public async Task<bool> IsEmailAddressUsed(string email)
    {
        bool addressUsed = false;
        string emailToJson = JsonSerializer.Serialize(email);

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
                if (userFromDb.EmailAddress.Equals(email))
                    addressUsed = true;
            }
        }

        return addressUsed;
    }
}