using System.Text.Json.Serialization;

namespace Model.HttpResponse;

public class HttpResponseCredits
{
    [JsonPropertyName("cast")] 
    public IList<HttpResponseCast> castMembers { get; set; }

    [JsonPropertyName("crew")]
    public IList<HttpResponseCrew> crewMembers { get; set; }
}
