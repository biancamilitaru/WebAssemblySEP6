using System.Collections.Generic;
using System.Threading.Tasks;
using Model;

namespace WebAssemblySEP6.Communication
{

    public interface ITopListCommunication
    {
      //  public Task<IList<TopList>> GetAllTopListsAsync(int userID);
        public Task AddTopListAsync(TopList topList);
        public Task<bool> IsIdCorrect(TopList topList);
    }
}