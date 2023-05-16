using System.Text.Json.Serialization;

namespace Model.HttpResponse;

public class HttpResponseCast
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("character")]
    public string Character { get; set; }
}