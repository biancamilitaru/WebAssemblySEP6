using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Model;
using WebAssemblySEP6.Communication;
using WebAssemblySEP6.Model;

namespace WebAssemblySEP6.Pages
{

    
    public partial class IndividualMovie
    {
        [Inject]
    public NavigationManager NavigationManager { get; set; }
    private IIndividualMovieCommunication individualMovieCommunication;
    private ICommentCommunication commentCommunication;
    private IUserCommunication userCommunication;
    [Parameter] public int movieId { get; set; }

    private IList<Comment> comments = new List<Comment>();

    private Movie movie = new();
    private User user = new();

     protected override async Task OnInitializedAsync()
    {
        individualMovieCommunication = new IndividualMovieCommunication();
        commentCommunication = new CommentCommunication();
        userCommunication = new UserCommunication();

        movie = await individualMovieCommunication.GetMovieByIdAsync(movieId);
        comments = await commentCommunication.GetCommentsForMovie(movieId);
        foreach (Comment comment in comments)
        {
            Console.WriteLine(comment.CommentText);
        }
    }


    
    public async void AddComment() {
        var Title = movie.Title;
        var Id = movie.Id;
        NavigationManager.NavigateTo($"/add-comment/{Title}/{Id}");
    }


    }
}