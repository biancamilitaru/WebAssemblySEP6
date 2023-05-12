using System.Text.Json.Serialization;

namespace WebAssemblySEP6.Model.HttpResponse;

public class HttpResponseCredits
{
    [JsonPropertyName("cast")] 
    public IList<HttpResponseCast> castMembers { get; set; }

    [JsonPropertyName("crew")]
    public IList<HttpResponseCrew> crewMembers { get; set; }
}
