using WebAssemblySEP6.Communication;
using Model;

namespace WebAssemblySEP6.Pages
{

    public partial class TopMovies
    {
        private ITopMoviesCommunication topMoviesCommunication = new TopMovieCommunication();

        private IList<Movie> movies = new List<Movie>();

        protected override async Task OnInitializedAsync()
        {
            movies = await topMoviesCommunication.GetTopMoviesAsync();
        }
    }
}