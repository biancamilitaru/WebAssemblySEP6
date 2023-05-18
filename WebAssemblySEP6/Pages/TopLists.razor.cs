using System;
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
        private ITopListMovieCommunication topListMovieCommunication = new TopListMovieCommunication();

         [Inject]
        public NavigationManager NavigationManager {get;set;}

        private IList<TopList> topLists = new List<TopList>();

        protected override async Task OnInitializedAsync()
        {
            int id = 4;
            topLists = await topListCommunication.GetAllTopListsByIdAsync(id);
        }

        private async Task DeleteTopList(int id, string topListName)
        {
            NavigationManager.NavigateTo($"/delete-toplist/{id}?topListName={Uri.EscapeDataString(topListName)}");
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