using System.Text.Json.Serialization;

namespace WebAssemblySEP6.Model.HttpResponse
{

    public class HttpResponseCrew
    {
        [JsonPropertyName("name")] public string Name { get; set; }
        [JsonPropertyName("job")] public string Job { get; set; }
    }
}