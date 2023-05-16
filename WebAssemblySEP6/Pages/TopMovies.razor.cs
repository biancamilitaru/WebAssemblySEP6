using System.Collections.Generic;
using System.Threading.Tasks;
using Model;
using WebAssemblySEP6.Communication;
using WebAssemblySEP6.Model;

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