using System.Collections.Generic;
using System.Threading.Tasks;
using Model;
using WebAssemblySEP6.Model;

namespace SEP6Backend.DataAccess
{
    public interface ITopListDataAccess
    {
        public Task AddTopListAsync(TopList topList);

        public Task<IList<TopList>> GetAllTopListsByIdAsync(int userId);
        
        public Task<IList<TopList>> IsIdCorrect();
        
        public Task DeleteTopListById(int id);
    }
}