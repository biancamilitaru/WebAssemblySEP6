using System.Text.Json.Serialization;

namespace Model.HttpResponse
{

    public class HttpResponseTopMovies
    {
        [JsonPropertyName("results")] public IList<HttpResponseTopMovie> TopMovies { get; set; }
    }
}