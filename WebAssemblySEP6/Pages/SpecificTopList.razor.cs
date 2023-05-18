using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Model;
using WebAssemblySEP6.Communication;

namespace WebAssemblySEP6.Pages
{
    public partial class SpecificTopList
    {
        [Parameter] public int topListId { get; set; }
        [Parameter] public string topListName { get; set; }
        private ITopListCommunication topListCommunication = new TopListCommunication();
        private ITopListMovieCommunication topListMovieCommunication = new TopListMovieCommunication();
        
        private IList<Movie> movies = new List<Movie>();
        
        [Inject]
        public NavigationManager NavigationManager {get;set;}
        
        
        protected override async Task OnInitializedAsync()
        {
            var uri = new Uri(NavigationManager.Uri);
            var queryString = uri.Query.TrimStart('?');
            var parsedQuery = System.Web.HttpUtility.ParseQueryString(queryString);
            topListName = parsedQuery["topListName"];

            base.OnInitialized();
            Console.WriteLine($"This is the id: {topListId}");
            
            movies = await topListMovieCommunication.GetMoviesForATopList(topListId);
        }
        
        private async Task openAddTopListPage()
        {
            NavigationManager.NavigateTo("/CreateToplist");
        }
        
        private void openTopListPage()
        {

            NavigationManager.NavigateTo("/top-lists");
        }
        
        private async Task DeleteTopList()
        {
            NavigationManager.NavigateTo($"/delete-toplist/{topListId}?topListName={Uri.EscapeDataString(topListName)}");
        }

        
    }
    
}