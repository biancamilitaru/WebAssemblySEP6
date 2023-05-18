using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using WebAssemblySEP6.Communication;
using Model;
using Microsoft.AspNetCore.Components;

namespace WebAssemblySEP6.Pages
{

    public partial class DeleteTopList
    {
        private ITopListCommunication topListCommunication = new TopListCommunication();
        private ITopListMovieCommunication topListMovieCommunication = new TopListMovieCommunication();
        //Todo: communication class
        [Parameter]
        public string topListName {get;set;}
        [Parameter]
        public int id {get;set;}

         [Inject]
        public NavigationManager NavigationManager {get;set;}

        protected override void OnInitialized()
        {
            var uri = new Uri(NavigationManager.Uri);
            var queryString = uri.Query.TrimStart('?');
            var parsedQuery = System.Web.HttpUtility.ParseQueryString(queryString);
            topListName = parsedQuery["topListName"];

            base.OnInitialized();
            Console.WriteLine($"This is the id: {id}");
        }

        private async Task deleteTopList()
        {
            await topListMovieCommunication.DeleteTopListMovieById(id);
            await topListCommunication.DeleteTopListById(id);
            NavigationManager.NavigateTo("/top-lists");
        }

        private void doNotDeleteTopList()
        {
            NavigationManager.NavigateTo("/top-lists");
        }
    }
}