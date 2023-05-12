using System.Text.Json.Serialization;

namespace WebAssemblySEP6.Model.HttpResponse;

public class HttpResponseTopMovies
{
    [JsonPropertyName("results")] 
    public IList<HttpResponseTopMovie> TopMovies { get; set; }
}