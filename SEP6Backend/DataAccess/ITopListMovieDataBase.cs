using System.Collections.Generic;
using System.Threading.Tasks;
using Model;
using WebAssemblySEP6.Model;

namespace SEP6Backend.DataAccess
{
    public interface ITopListMovieDataBase
    {
        public Task AddTopListMoviesAsyncList(TopList topList, List<Movie> movies);
        public Task DeleteTopListMovieByIdAsync(int id);
        
        public Task<IList<Movie>> GetMoviesForATopList(int topListID);
    }
}