using System.Text.Json.Serialization;

namespace WebAssemblySEP6.Model.HttpResponse;

public class HttpResponseMovieDetails
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("original_title")]
    public string Title { get; set; }
    [JsonPropertyName("overview")]
    public string Description { get; set; }
    [JsonPropertyName("poster_path")]
    public string Image { get; set; }
    [JsonPropertyName("vote_average")]
    public double AvgRating { get; set; }
    [JsonPropertyName("vote_count")]
    public int NumberOfVotes { get; set; }
}