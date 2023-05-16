using System.Threading.Tasks;
using Model;

namespace WebAssemblySEP6.Communication
{

    public interface IForecastCommunication
    {
        public Task<WeatherForecast[]> GetForecast();
    }
}
// TODO - delete this file before deploy