using System.Collections.Generic;
using System.Threading.Tasks;
using WebAssemblySEP6.Model;
using WebAssemblySEP6.Communication;

namespace WebAssemblySEP6.Pages
{
    public partial class CreateTopList
    {
        private IIndividualMovieCommunication individualMovieCommunication = new IndividualMovieCommunication();
        private TopList topList = new TopList();
        private string topListTitle;
        private string searchText;
        private IList<int> searchedMovies = new List<int>();
        private IList<int> selectedMovies = new List<int>();
        public int[] movieIds = new[] {76600, 447365, 502356, 713704, 299534};

        protected async Task AddToplistToDB()
        {
           
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
            selectedMovies.Add(id);
        }
        
        private async Task RemoveFromSelected(int id)
        {
            selectedMovies.Remove(id); 
            //StateHasChanged();
                
            
        }
    }
}