using System.Text.Json.Serialization;

namespace Model.HttpResponse
{
    public class HttpResponseSearchMovie
    {
        [JsonPropertyName("results")] public IList<HttpResponseMovieDetails> results { get; set; }
    }
}