using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using WebAssemblySEP6.Communication;
using WebAssemblySEP6.Model;
using Microsoft.AspNetCore.Components;

namespace WebAssemblySEP6.Pages
{

    public partial class DeleteTopList
    {
        
        //Todo: communication class
        [Parameter]
        public string topListName {get;set;}
        [Parameter]
        public int topListId{get;set;}

         [Inject]
        public NavigationManager NavigationManager {get;set;}


        private void deleteTopList()
        {
            //Todo: delete operation
        }

        private void doNotDeleteTopList()
        {
            NavigationManager.NavigateTo("/top-lists");
        }
    }
}