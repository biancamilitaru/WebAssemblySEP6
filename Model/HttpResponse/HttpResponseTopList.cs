using System.Text.Json.Serialization;

namespace WebAssemblySEP6.Model.HttpResponse
{
    public class HttpResponseTopList
    {
        [JsonPropertyName("topListId")] public int Id { get; set; }
        [JsonPropertyName("name")] public string Title { get; set; }
        [JsonPropertyName("userFk")] public int UserName { get; set; }
    }
}