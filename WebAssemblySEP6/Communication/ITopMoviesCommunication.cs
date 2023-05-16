using System.Collections.Generic;
using System.Threading.Tasks;
using Model;
using WebAssemblySEP6.Model;
using Model;

namespace WebAssemblySEP6.Communication
{

    public interface ITopMoviesCommunication
    {
        public Task<IList<Movie>> GetTopMoviesAsync();
    }
}