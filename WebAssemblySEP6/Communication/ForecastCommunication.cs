using System.Net.Http;
using Model;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace WebAssemblySEP6.Communication
{

    public class ForecastCommunication : IForecastCommunication
    {
        private HttpClient httpClient = new();

        public async Task<WeatherForecast[]> GetForecast()
        {
            // this http request throws error for wrong path - ignore and do not use
            return await httpClient.GetFromJsonAsync<WeatherForecast[]>(
                "/Users/bianca/RiderProjects/WebAssemblySEP6/WebAssemblySEP6/wwwroot/sample-data/weather.json");
        }
    }
}
// TODO - delete this file before deploy