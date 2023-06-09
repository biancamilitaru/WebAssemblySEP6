using System.Collections.Generic;
using System.Threading.Tasks;
using Model;

namespace WebAssemblySEP6.Communication
{

    public interface IIndividualMovieCommunication
    {
        public Task<Movie> GetMovieByIdAsync(int id);
        public Task<IList<int>> GetMoviesBySearchName(string name);
    }
}