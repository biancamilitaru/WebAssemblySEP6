using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using WebAssemblySEP6.Communication;
using WebAssemblySEP6.Model;

namespace WebAssemblySEP6.Pages
{
    public partial class TopListMovieCard
    {
        
        [Parameter] public int movieId { get; set; }

        [Parameter] public Movie movie { get; set; } = new Movie();

        private IIndividualMovieCommunication individualMovieCommunication = new IndividualMovieCommunication();

        [Inject]
        public NavigationManager NavigationManager {get;set;}

        protected override async Task OnInitializedAsync()
        {
            movie = await individualMovieCommunication.GetMovieByIdAsync(movieId);
        }

        
        private async Task GetIndividualMovie()
        {
            NavigationManager.NavigateTo($"/movie/{movieId}");
        }
    }
}