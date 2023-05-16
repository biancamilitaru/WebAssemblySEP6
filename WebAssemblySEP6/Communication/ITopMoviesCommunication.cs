using System.Collections.Generic;
using System.Threading.Tasks;
using WebAssemblySEP6.Model;

namespace WebAssemblySEP6.Communication
{

    public interface ITopMoviesCommunication
    {
        public Task<IList<Movie>> GetTopMoviesAsync();
    }
}