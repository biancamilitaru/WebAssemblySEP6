using System.Collections.Generic;
using System.Threading.Tasks;
using WebAssemblySEP6.Communication;
using WebAssemblySEP6.Model;

namespace WebAssemblySEP6.Pages
{

    public partial class TopLists
    {
        public int Id;
        private ITopListCommunication topListCommunication = new TopListCommunication();

        private IList<TopList> topLists = new List<TopList>();

        protected override async Task OnInitializedAsync()
        {
            topLists=  topListCommunication.GetTopListsAsync();
        }

        private async Task deleteTopList()
        {
                
        }
    }
}