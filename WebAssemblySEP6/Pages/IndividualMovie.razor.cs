
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebAssemblySEP6.Communication;
using WebAssemblySEP6.Model;

namespace WebAssemblySEP6.Pages;

public partial class IndividualMovie
{
    private IIndividualMovieCommunication individualMovieCommunication = new IndividualMovieCommunication();

    private Movie movie = new();
    
    protected override async Task OnInitializedAsync()
    {
        movie = await individualMovieCommunication.GetMovieAsync(1);
    }
    
    public void AddComment() {

    }


}