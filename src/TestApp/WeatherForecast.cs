using System.Text.Json.Serialization;

namespace ExampleApp;

[JsonDerivedType(typeof(SummayWeatherForecast), "summary")]
[JsonDerivedType(typeof(ExtendedWeatherForecast), "extended")]
public class WeatherForecast
{
    public DateOnly Date { get; set; }
}

public class SummayWeatherForecast : WeatherForecast
{
    public string Summary { get; set; }

    public SummayWeatherForecast(string summary)
    {
        Summary = summary;
    }
}

public class ExtendedWeatherForecast : SummayWeatherForecast
{
    public int TemperatureC { get; set; }

    public ExtendedWeatherForecast(string summary) : base(summary)
    {
    }
}
