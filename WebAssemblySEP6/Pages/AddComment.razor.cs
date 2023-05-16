using Microsoft.AspNetCore.Components;
using WebAssemblySEP6.Communication;
using Model;

namespace WebAssemblySEP6.Pages;

public partial class AddComment
{
        [Inject]
        private NavigationManager navigationManager { get; set; }
        private ICommentCommunication commentCommunication { get; set; }

        private string commentText;
        private Comment comment;
        [Parameter]
        public string? Title {get;set;}

        [Parameter]
        public int Id {get;set;}


       protected override void OnInitialized()
        {
            commentCommunication = new CommentCommunication();
            comment = new Comment();
            
        }


        public async Task postCommentAsync()
        {

            comment = new Comment{
                UserId =1,
                MovieId=Id,
                CommentText=commentText,

            };
            await commentCommunication.IncreaseCommendId(comment);
            Console.WriteLine("Sending comment to db");
            await commentCommunication.AddCommentAsync(comment);
            var movieId = comment.MovieId;
            Console.WriteLine("!!!!!"+movieId);
            navigationManager.NavigateTo("/movie/{moviId}");
            
        }

    
}