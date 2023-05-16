using System.Collections.Generic;
using System.Threading.Tasks;
using Model;
using WebAssemblySEP6.Model;

namespace WebAssemblySEP6.Communication
{
    public interface ITopListCommunication
    {
        public Task AddTopListAsync(TopList topList);
        public Task<bool> IsIdCorrect(TopList topList);
        
    }
}