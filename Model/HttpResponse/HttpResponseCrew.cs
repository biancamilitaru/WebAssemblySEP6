using System.Text.Json.Serialization;

namespace Model.HttpResponse
{

    public class HttpResponseCrew
    {
        [JsonPropertyName("name")] public string Name { get; set; }
        [JsonPropertyName("job")] public string Job { get; set; }
    }
}