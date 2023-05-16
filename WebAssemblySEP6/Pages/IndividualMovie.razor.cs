using WebAssemblySEP6.Communication;
using Microsoft.AspNetCore.Components;
using Model;

namespace WebAssemblySEP6.Pages
{
    public partial class IndividualMovie
    {
        private IIndividualMovieCommunication individualMovieCommunication = new IndividualMovieCommunication();
        [Parameter] public int movieId { get; set; }

        private Movie movie = new();

        protected override async Task OnInitializedAsync()
        {
            movie = await individualMovieCommunication.GetMovieByIdAsync(movieId);
        }

        public void AddComment()
        {

        }
    }

}