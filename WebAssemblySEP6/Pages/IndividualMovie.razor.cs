using WebAssemblySEP6.Communication;
using Model;

namespace WebAssemblySEP6.Pages;

public partial class IndividualMovie
{
    private IIndividualMovieCommunication individualMovieCommunication = new IndividualMovieCommunication();

    private Movie movie = new();
    
    protected override async Task OnInitializedAsync()
    {
        movie = await individualMovieCommunication.GetMovieAsync(118340);
    }
    
    public void AddComment() {

    }


}