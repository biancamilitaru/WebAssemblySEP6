
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using WebAssemblySEP6.Communication;
using WebAssemblySEP6.Model;



namespace WebAssemblySEP6.Pages;

public partial class IndividualMovie
{

    [Inject]
    public NavigationManager NavigationManager { get; set; }
    private IIndividualMovieCommunication individualMovieCommunication = new IndividualMovieCommunication();

    private Movie movie = new();


    
    protected override async Task OnInitializedAsync()
    {
        movie = await individualMovieCommunication.GetMovieAsync(118340);
    }
    
    public async void AddComment() {
        var Title = movie.Title;
        var Id = movie.Id;
        NavigationManager.NavigateTo($"/add-comment/{Title}/{Id}");
    }


}