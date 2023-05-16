using System.Text;
using System.Text.Json;
using Model;

namespace WebAssemblySEP6.Communication;

public class UserCommunication :IUserCommunication
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
}