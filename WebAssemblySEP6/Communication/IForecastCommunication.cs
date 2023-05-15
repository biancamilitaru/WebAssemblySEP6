using Model;

namespace WebAssemblySEP6.Communication;

public interface IForecastCommunication
{
    public Task<WeatherForecast[]> GetForecast();
}