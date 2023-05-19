using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using WebAssemblySEP6.Communication;
using Model;
using WebAssemblySEP6.Authentication;

namespace WebAssemblySEP6.Pages
{
    [Authorize]
    public partial class TopLists
    {
        [Parameter] public int topListId { get; set; }
        [Parameter] public string topListName { get; set; }
        private ITopListCommunication topListCommunication = new TopListCommunication();
        private ITopListMovieCommunication topListMovieCommunication = new TopListMovieCommunication();
        
        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        

         [Inject]
        public NavigationManager NavigationManager {get;set;}

        private IList<TopList> topLists = new List<TopList>();

        private int userIdFromLogin {get;set;}
        
        
        

        protected override async Task OnInitializedAsync()
        {
            userIdFromLogin = ((CustomAuthenticationStateProvider) AuthenticationStateProvider).CachedUser.UserId;
            topLists = await topListCommunication.GetAllTopListsByIdAsync(userIdFromLogin);
        }

        private async Task DeleteTopList(int id, string topListName)
        {
            NavigationManager.NavigateTo($"/delete-toplist/{id}?topListName={Uri.EscapeDataString(topListName)}");
        }

         private async Task openAddTopListPage()
        {
            NavigationManager.NavigateTo("/CreateToplist");
        }

        private void openTopListPage(int id, string topListName)
        {

            NavigationManager.NavigateTo($"/SpecificTopList/{id}?topListName={Uri.EscapeDataString(topListName)}");
        }



        
    }
}