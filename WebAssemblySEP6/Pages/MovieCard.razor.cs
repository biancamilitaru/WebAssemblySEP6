using Microsoft.AspNetCore.Components;
using WebAssemblySEP6.Communication;
using WebAssemblySEP6.Model;

namespace WebAssemblySEP6.Pages;

public partial class MovieCard
{
    [Parameter] 
    public int movieId { get; set; }
    [Parameter]
    public Movie movie { get; set; }

    private IMoviePlaceHolder moviePlaceHolder = new MoviePlaceHolder();

    protected override async Task OnInitializedAsync()
    {
        movie = moviePlaceHolder.GetMovieById(movieId);
    }
}