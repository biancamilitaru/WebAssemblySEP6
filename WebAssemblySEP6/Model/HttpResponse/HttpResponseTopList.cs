using System.Text.Json.Serialization;

namespace WebAssemblySEP6.Model.HttpResponse
{
    public class HttpResponseTopList
    {
        [JsonPropertyName("id")] public int Id { get; set; }
        [JsonPropertyName("original_title")] public string Title { get; set; }
        [JsonPropertyName("original_title")] public string UserName { get; set; }
    }
}