using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Model;
using WebAssemblySEP6.Authentication;
using WebAssemblySEP6.Model;
using WebAssemblySEP6.Communication;

namespace WebAssemblySEP6.Pages
{
    [Authorize]
    public partial class CreateTopList
    {
        
        private IIndividualMovieCommunication individualMovieCommunication { get; set; }
        private ITopListCommunication topListCommunication { get; set; }
        private ITopListMovieCommunication topListMovieCommunication { get; set; }
        [Inject] private NavigationManager navigationManager { get; set; }
        private TopList topList;
        
        private string searchText;
        private IList<int> searchedMovies = new List<int>();
        private List<Movie> selectedMovies = new List<Movie>();
        public int[] movieIds = new[] {76600, 447365, 502356, 713704, 299534};
        
        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        private int userIdFromLogin {get;set;}
        


        protected override void OnInitialized()
        {
            individualMovieCommunication = new IndividualMovieCommunication();
            topListCommunication = new TopListCommunication();
            topListMovieCommunication = new TopListMovieCommunication();
            topList = new TopList();
            userIdFromLogin = ((CustomAuthenticationStateProvider) AuthenticationStateProvider).CachedUser.UserId;
            topList.UserName = userIdFromLogin;
        }
        
        
        private async Task Search()
        {
            searchedMovies = await  individualMovieCommunication.GetMoviesBySearchName("");
            await InvokeAsync(StateHasChanged);
            searchedMovies = await  individualMovieCommunication.GetMoviesBySearchName(searchText);
            await InvokeAsync(StateHasChanged);
                
        }

        private async Task AddToTopList(int id)
        {
            if (selectedMovies.Any(m => m.Id == id))
            {
                Console.WriteLine($"Movie with ID {id} already exists in the list.");
                return;
            }
            
            Movie movie = await individualMovieCommunication.GetMovieByIdAsync(id);
            selectedMovies.Add(movie);
        }
        
        private async Task RemoveFromSelected(Movie movie)
        {
            selectedMovies.Remove(movie);
            for (int i = 0; i < selectedMovies.Count; i++)
            {
                Console.WriteLine(selectedMovies[i].Title);

            }
        }
        
        
        public async Task AddToplistToDB()
        {
            if (await topListCommunication.IsIdCorrect(topList) == true)
            {
                Console.WriteLine("New Id is created");
                await topListCommunication.AddTopListAsync(topList);
                await topListMovieCommunication.AddTopListMoviesAsyncList(topList, selectedMovies);
            }
            navigationManager.NavigateTo("/top-lists");
        }
    }
}