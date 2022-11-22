

[![build](https://github.com/marcelbarner/AspNetCore.Json.SinglePolymorphicResponse/actions/workflows/main.yml/badge.svg)](https://github.com/marcelbarner/AspNetCore.Json.SinglePolymorphicResponse/actions/workflows/main.yml)
![](https://img.shields.io/nuget/v/AspNetCore.Json.SinglePolymorphicResponse)
[![](https://img.shields.io/github/release/Fmarcelbarner/AspNetCore.Json.SinglePolymorphicResponse.svg?label=latest%20release)](https://github.com/marcelbarner/AspNetCore.Json.SinglePolymorphicResponse/releases/latest)
[![](https://img.shields.io/nuget/dt/AspNetCore.Json.SinglePolymorphicResponse.svg?label=nuget%20downloads)](https://www.nuget.org/packages/AspNetCore.Json.SinglePolymorphicResponse)
[![](https://img.shields.io/librariesio/dependents/nuget/AspNetCore.Json.SinglePolymorphicResponse.svg?label=dependent%20libraries)](https://libraries.io/nuget/AspNetCore.Json.SinglePolymorphicResponse)
![](https://img.shields.io/badge/release%20strategy-githubflow-orange.svg)
[![Coverage Status](https://coveralls.io/repos/github/marcelbarner/AspNetCore.Json.SinglePolymorphicResponse/badge.svg?branch=master)](https://coveralls.io/github/marcelbarner/AspNetCore.Json.SinglePolymorphicResponse?branch=main)

# Usage

1. Install the package
```bash
dotnet add package AspNetCore.Json.SinglePolymorphicResponse
```

2. Adjust your Program.cs and add
```cs
builder.Services.AddSinglePolymorphicJsonResponse();
```

# Original Problem

Without this package it is not possible to identify the original response type by discriminator when only a single object is returned.
This problem is also open as an [issue in the aspnetcore repo](https://github.com/dotnet/aspnetcore/issues/44852), but it looks like that they will fix it only with .NET 8.

## Defined response
```cs
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
```
## Defined API
```cs
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
```

## Current behaviour
### Response of route '/WeatherForecast'
```json
[
  {
    "$type": "extended",
    "temperatureC": -15,
    "summary": "Freezing",
    "date": "2022-11-21"
  },
  {
    "$type": "summary",
    "summary": "Mild",
    "date": "2022-11-20"
  }
]
```
### Response of route '/WeatherForecast/summary'
```json
{
  "summary": "Mild",
  "date": "2022-11-20"
}
```
### Response of route '/WeatherForecast/extended'
```json
{
  "temperatureC": -15,
  "summary": "Freezing",
  "date": "2022-11-21"
}
```
## Expected behaviour
### Response of route '/WeatherForecast'
```json
[
  {
    "$type": "extended",
    "temperatureC": -15,
    "summary": "Freezing",
    "date": "2022-11-21"
  },
  {
    "$type": "summary",
    "summary": "Mild",
    "date": "2022-11-20"
  }
]
```
### Response of route '/WeatherForecast/summary'
```json
{
  "$type": "summary",
  "summary": "Mild",
  "date": "2022-11-20"
}
```
### Response of route '/WeatherForecast/extended'
```json
{
  "$type": "extended",
  "temperatureC": -15,
  "summary": "Freezing",
  "date": "2022-11-21"
}
```