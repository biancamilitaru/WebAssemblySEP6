using System.Text.Json.Serialization;

namespace WebAssemblySEP6.Model.HttpResponse;

public class HttpResponseTopMovie
{
    [JsonPropertyName("original_title")]
    public string Title { get; set; }
    [JsonPropertyName("overview")]
    public string Description { get; set; }
    [JsonPropertyName("poster_path")]
    public string Image { get; set; }
  
}