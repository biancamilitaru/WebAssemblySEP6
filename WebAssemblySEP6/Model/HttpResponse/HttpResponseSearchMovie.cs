using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebAssemblySEP6.Model.HttpResponse
{

    public class HttpResponseSearchMovie
    {
        [JsonPropertyName("results")] public IList<HttpResponseMovieDetails> results { get; set; }
    }
}