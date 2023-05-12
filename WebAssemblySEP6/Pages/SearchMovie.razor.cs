using WebAssemblySEP6.Communication;

namespace WebAssemblySEP6.Pages;

public partial class SearchMovie
{
    private IIndividualMovieCommunication individualMovieCommunication = new IndividualMovieCommunication();
    private string searchText;
    private IList<int> searchedMovies = new List<int>();
    public int[] movieIds = new[] {76600, 447365, 502356};

    private async void Search()
    {
        searchedMovies = await individualMovieCommunication.GetMoviesBySearchName(searchText);
    }
}