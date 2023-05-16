using System.Text.Json.Serialization;

namespace Model
{

    public class User
    {
        [JsonPropertyName("userId")] public int UserId { get; set; }
        [JsonPropertyName("email")] public string EmailAddress { get; set; }
        [JsonPropertyName("password")] public string Password { get; set; }
        [JsonPropertyName("name")] public string Name { get; set; }
    }
}