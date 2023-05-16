using System.Collections.Generic;
using System.Text.Json.Serialization;
using WebAssemblySEP6.Model;

namespace Model
{
    public class TopListWithMovies
    {
        [JsonPropertyName("topList")] public TopList TopList { get; set; }
        
        [JsonPropertyName("movies")] public List<Movie> Movies { get; set; }
    }
}