using System.Threading.Tasks;
using WebAssemblySEP6.Model;
using WebAssemblySEP6.Communication;

namespace WebAssemblySEP6.Pages
{

    public partial class FetchData
    {
        private IForecastCommunication communication = new ForecastCommunication();
        private WeatherForecast[]? forecasts;

        protected override async Task OnInitializedAsync()
        {
            //forecasts = await communication.GetForecast();
        }
    }
}