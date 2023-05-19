using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using WebAssemblySEP6.Communication;
using Model;
using WebAssemblySEP6.Authentication;

namespace WebAssemblySEP6.Pages
{
    [Authorize]
    public partial class AddComment
    {
        [Inject] private NavigationManager navigationManager { get; set; }
        private ICommentCommunication commentCommunication { get; set; }

        private string commentText;
        private Comment comment;
        [Parameter] public string? Title { get; set; }

        [Parameter] public int Id { get; set; }
        
        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        private int userIdFromLogin {get;set;}


        protected override void OnInitialized()
        {
            commentCommunication = new CommentCommunication();
            comment = new Comment();
            userIdFromLogin = ((CustomAuthenticationStateProvider) AuthenticationStateProvider).CachedUser.UserId;
        }


        public async Task postCommentAsync()
        {

            comment = new Comment
            {
                UserId = userIdFromLogin,
                MovieId = Id,
                CommentText = commentText,

            };
            await commentCommunication.IncreaseCommendId(comment);
            await commentCommunication.AddCommentAsync(comment);
            var movieId = comment.MovieId;
            navigationManager.NavigateTo($"/movie/{movieId}");

        }


    }
}