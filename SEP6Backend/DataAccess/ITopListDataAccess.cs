using System.Collections.Generic;
using System.Threading.Tasks;
using WebAssemblySEP6.Model;

namespace SEP6Backend.DataAccess
{
    public interface ITopListDataAccess
    {
        public Task AddTopListAsync(TopList topList);
        public Task<IList<TopList>> GetAllTopListAsync();
    }
}