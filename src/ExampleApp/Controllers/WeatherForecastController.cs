using Microsoft.AspNetCore.Mvc;

namespace ExampleApp.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    [HttpGet()]
    public IEnumerable<WeatherForecast> Get()
    {
        var forecast1 = new ExtendedWeatherForecast("Freezing") { Date = DateOnly.Parse("2022-11-21"), TemperatureC = -15 };
        var forecast2 = new SummayWeatherForecast("Mild") { Date = DateOnly.Parse("2022-11-20") };
        return new WeatherForecast[] { forecast1, forecast2 };
    }

    [HttpGet("Summary")]
    public WeatherForecast GetSummary()
    {
        var forecast2 = new SummayWeatherForecast("Mild") { Date = DateOnly.Parse("2022-11-20") };
        return forecast2;
    }

    [HttpGet("Extended")]
    public WeatherForecast GetExtended()
    {
        var forecast1 = new ExtendedWeatherForecast("Freezing") { Date = DateOnly.Parse("2022-11-21"), TemperatureC = -15 };
        return forecast1;
    }
}
