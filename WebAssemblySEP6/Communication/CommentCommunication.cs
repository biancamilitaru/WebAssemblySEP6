using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Model;

namespace WebAssemblySEP6.Communication
{

    public class CommentCommunication : ICommentCommunication
    {
        // TODO - update this links with the good ones after deploying the web online
        private string uri = "https://localhost:7044/Comment";
        private HttpClient httpClient;

        public CommentCommunication()
        {
            httpClient = new HttpClient();
        }

        public async Task AddCommentAsync(Comment commentToAdd)
        {

            string userToAddAsJson = JsonSerializer.Serialize(commentToAdd);

            StringContent content = new StringContent(
                userToAddAsJson,
                Encoding.UTF8,
                "application/json"
            );

            HttpResponseMessage responseMessage = await httpClient.PostAsync(uri, content);
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception($"Error: {responseMessage.StatusCode}, {responseMessage.ReasonPhrase}");
            }
        }

        public async Task<Comment> GetCommentById(int commentId)
        {
            HttpResponseMessage responseMessage = await httpClient.GetAsync($"{uri}/commentId/{commentId}");
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception($"Error: {responseMessage.StatusCode}, {responseMessage.ReasonPhrase}");
            }

            var responseStream = await responseMessage.Content.ReadAsStreamAsync();

            var httpResponse = JsonSerializer.Deserialize<Comment>(responseStream,
                new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNameCaseInsensitive = true
                });

            return httpResponse;
        }

        public async Task<IList<Comment>> GetCommentsForMovie(int movieId)
        {
            HttpResponseMessage responseMessage = await httpClient.GetAsync($"{uri}/{movieId}");
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception($"Error: {responseMessage.StatusCode}, {responseMessage.ReasonPhrase}");
            }

            var responseStream = await responseMessage.Content.ReadAsStreamAsync();

            var httpResponse = JsonSerializer.Deserialize<IList<Comment>>(responseStream,
                new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNameCaseInsensitive = true
                });

            return httpResponse;
        }

        public async Task IncreaseCommendId(Comment comment)
        {

            var comments = await GetAllComments();

            if (comments != null)
            {

                comment.CommentId = comments.Last().CommentId + 1;
            }
        }

        public async Task<IList<Comment>> GetAllComments()
        {
            HttpResponseMessage responseMessage = await httpClient.GetAsync(uri);
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception($"Error: {responseMessage.StatusCode}, {responseMessage.ReasonPhrase}");
            }

            var responseStream = await responseMessage.Content.ReadAsStreamAsync();

            var httpResponse = JsonSerializer.Deserialize<IList<Comment>>(responseStream,
                new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNameCaseInsensitive = true
                });

            return httpResponse;
        }
    }
}


   