using System.Collections.Generic;
using System.Threading.Tasks;
using Model;
using WebAssemblySEP6.Model;

namespace WebAssemblySEP6.Communication
{
    public interface ITopListMovieCommunication
    {
        public Task AddTopListMoviesAsyncList(TopList topList, List<Movie> movies);
    }
}