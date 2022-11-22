using Microsoft.AspNetCore.Mvc;

namespace ExampleApp.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    [HttpGet("enumerable")]
    public IEnumerable<WeatherForecast> GetEnumerable()
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

    [HttpGet("Task/Enumerable")]
    public Task<IEnumerable<WeatherForecast>> GetTaskEnumerable()
    {
        var forecast1 = new ExtendedWeatherForecast("Freezing") { Date = DateOnly.Parse("2022-11-21"), TemperatureC = -15 };
        var forecast2 = new SummayWeatherForecast("Mild") { Date = DateOnly.Parse("2022-11-20") };
        return Task.FromResult<IEnumerable<WeatherForecast>>(new WeatherForecast[] { forecast1, forecast2 });
    }

    [HttpGet("Task/Summary")]
    public Task<WeatherForecast> GetTaskSummary()
    {
        var forecast2 = new SummayWeatherForecast("Mild") { Date = DateOnly.Parse("2022-11-20") };
        return Task.FromResult<WeatherForecast>(forecast2);
    }

    [HttpGet("Task/Extended")]
    public Task<WeatherForecast> GetTaskExtended()
    {
        var forecast1 = new ExtendedWeatherForecast("Freezing") { Date = DateOnly.Parse("2022-11-21"), TemperatureC = -15 };
        return Task.FromResult<WeatherForecast>(forecast1);
    }

    [HttpGet("ValueTask/Enumerable")]
    public ValueTask<IEnumerable<WeatherForecast>> GetValueTaskEnumerable()
    {
        var forecast1 = new ExtendedWeatherForecast("Freezing") { Date = DateOnly.Parse("2022-11-21"), TemperatureC = -15 };
        var forecast2 = new SummayWeatherForecast("Mild") { Date = DateOnly.Parse("2022-11-20") };
        return new ValueTask<IEnumerable<WeatherForecast>>(new WeatherForecast[] { forecast1, forecast2 });
    }

    [HttpGet("ValueTask/Summary")]
    public ValueTask<WeatherForecast> GetValueTaskSummary()
    {
        var forecast2 = new SummayWeatherForecast("Mild") { Date = DateOnly.Parse("2022-11-20") };
        return new ValueTask<WeatherForecast>(forecast2);
    }

    [HttpGet("ValueTask/Extended")]
    public ValueTask<WeatherForecast> GetValueTaskExtended()
    {
        var forecast1 = new ExtendedWeatherForecast("Freezing") { Date = DateOnly.Parse("2022-11-21"), TemperatureC = -15 };
        return new ValueTask<WeatherForecast>(forecast1);
    }

    [HttpGet("ActionResult/Ok/Enumerable")]
    public ActionResult<IEnumerable<WeatherForecast>> GetActionResultOkEnumerable()
    {
        var forecast1 = new ExtendedWeatherForecast("Freezing") { Date = DateOnly.Parse("2022-11-21"), TemperatureC = -15 };
        var forecast2 = new SummayWeatherForecast("Mild") { Date = DateOnly.Parse("2022-11-20") };
        return Ok(new WeatherForecast[] { forecast1, forecast2 });
    }

    [HttpGet("ActionResult/Ok/Summary")]
    public ActionResult<WeatherForecast> GetActionResultOkSummary()
    {
        var forecast2 = new SummayWeatherForecast("Mild") { Date = DateOnly.Parse("2022-11-20") };
        return Ok(forecast2);
    }

    [HttpGet("ActionResult/Ok/Extended")]
    public ActionResult<WeatherForecast> GetActionResultOkExtended()
    {
        var forecast1 = new ExtendedWeatherForecast("Freezing") { Date = DateOnly.Parse("2022-11-21"), TemperatureC = -15 };
        return Ok(forecast1);
    }
}
