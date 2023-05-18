using WebAssemblySEP6.Communication;
using Microsoft.AspNetCore.Components;
using Model;

namespace WebAssemblySEP6.Pages
{

    
    public partial class CommentComponent
    {
        
    private ICommentCommunication commentCommunication;
    private IUserCommunication userCommunication;

    private Comment comment =new();
    private User user=new();

    [Parameter]
    public int commentId {get;set;}

     protected override async Task OnInitializedAsync()
    {
        commentCommunication=new CommentCommunication();
        userCommunication= new UserCommunication();
        comment = await commentCommunication.GetCommentById(commentId);
        user = await userCommunication.GetUserById(comment.UserId);

    }

    }
}
