using WebAssemblySEP6.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebAssemblySEP6.Communication;

public class IndividualMovieCommunication : IIndividualMovieCommunication
{
    private HttpClient client = new();

    private string answer;



    public async Task GetMovie(int Id)
    {

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJjZjE2NTgwMTBmM2I3YzJhZTEyMDg4ZjYwYjA2OTk4NyIsInN1YiI6IjY0NWI4YTUxMWI3MGFlMDE0NWVlZmNiMyIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.qcTThPaLPgjcTtIC0ECwH2lcLBTGugvE4yEXGEOspDc");
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        string stringAsync = await client.GetStringAsync($"https://api.themoviedb.org/3/movie/{Id}?api_key=cf1658010f3b7c2ae12088f60b069987");
        Console.WriteLine(stringAsync.ToString());
        answer = stringAsync.ToString();

    }
}