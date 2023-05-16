using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using WebAssemblySEP6.Communication;
using Model;

namespace WebAssemblySEP6.Pages
{

    public partial class TopLists
    {
        [Parameter] public int topListId { get; set; }
        [Parameter] public string topListName { get; set; }
        private ITopListCommunication topListCommunication = new TopListCommunication();

         [Inject]
        public NavigationManager NavigationManager {get;set;}

        private IList<TopList> topLists = new List<TopList>();

        protected override async Task OnInitializedAsync()
        {
            topLists=  topListCommunication.GetTopListsAsync();
            //TODO: implement communication class
        }

        private async Task deleteTopList()
        {
            NavigationManager.NavigateTo($"/delete-toplist/{topListId}");
        }

         private async Task openAddTopListPage()
        {
            NavigationManager.NavigateTo("/CreateToplist");
        }

        private void openTopListPage()
        {

            NavigationManager.NavigateTo("/top-list");
        }



        
    }
}