using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using WebAssemblySEP6.Communication;

namespace WebAssemblySEP6.Pages
{

    public partial class SearchMovie
    {
        private IIndividualMovieCommunication individualMovieCommunication = new IndividualMovieCommunication();
        private string searchText;
        private IList<int> searchedMovies = new List<int>();
        public int[] movieIds = new[] {76600, 447365, 502356};

        private async Task Search()
        {
            searchedMovies = await  individualMovieCommunication.GetMoviesBySearchName("");
            await InvokeAsync(StateHasChanged);
            searchedMovies = await  individualMovieCommunication.GetMoviesBySearchName(searchText);
            await InvokeAsync(StateHasChanged);
                
        }
    }
}