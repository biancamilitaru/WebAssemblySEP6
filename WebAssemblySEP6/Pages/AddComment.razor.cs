using Microsoft.AspNetCore.Components;
using WebAssemblySEP6.Communication;
using Model;

namespace WebAssemblySEP6.Pages;

public partial class AddComment
{
    [Parameter]
    public string? Title {get;set;}

    [Parameter]
    public string? Id {get;set;}

    private string comment;

    //private IAddCommentCommunication addCommentCommunication = new AddCommentCommunication();

    public async void postCommentAsync(){
        
        //initialize comment and send it to the database
        //Open another page
    }

    
}