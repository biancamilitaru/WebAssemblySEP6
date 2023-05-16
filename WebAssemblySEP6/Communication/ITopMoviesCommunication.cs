using Model;

namespace WebAssemblySEP6.Communication
{

    public interface ITopMoviesCommunication
    {
        public Task<IList<Movie>> GetTopMoviesAsync();
    }
}