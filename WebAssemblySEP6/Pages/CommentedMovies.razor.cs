using Model;
using WebAssemblySEP6.Communication;

namespace WebAssemblySEP6.Pages;

public partial class CommentedMovies
{
    private ICommentCommunication commentCommunication = new CommentCommunication();
    private IIndividualMovieCommunication individualMovieCommunication = new IndividualMovieCommunication();
    private IList<Comment> allComments = new List<Comment>();
    private IOrderedEnumerable<IGrouping<int, Comment>> allCommentsGrouped;

    protected override async Task OnInitializedAsync()
    {
        allComments = await commentCommunication.GetAllComments();
    }
}