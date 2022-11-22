using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json.Linq;

namespace AspNetCore.Json.SinglePolymorphicResponse.Tests.IntegrationTests
{
    public class TestsWithoutPackage : IClassFixture<WebApplicationFactory<Program>>
    {
        public const string SummaryWeatherForecastJson = @"{""summary"":""Mild"", ""date"":""2022-11-20""}";
        public const string ExtendedWeatherForecastJson = @"{""summary"":""Freezing"", ""date"": ""2022-11-21"", ""TemperatureC"":-15}";
        public static JToken SummaryWeatherForecastResponse = JToken.Parse(SummaryWeatherForecastJson);
        public static JToken ExtendedWeatherForecastResponse = JToken.Parse(ExtendedWeatherForecastJson);
        public static JToken EnumerableWeatherForecastsResponse = JToken.Parse(@"[{""$type"":""summary"", ""summary"":""Mild"", ""date"":""2022-11-20""}, {""$type"":""extended"", ""summary"":""Freezing"", ""date"": ""2022-11-21"", ""TemperatureC"":-15}]");

        private readonly WebApplicationFactory<Program> fixture;

        public TestsWithoutPackage(WebApplicationFactory<Program> fixture)
        {
            this.fixture = fixture;
        }

        [Theory]
        [InlineData("WeatherForecast/enumerable")]
        [InlineData("WeatherForecast/task/enumerable")]
        [InlineData("WeatherForecast/valuetask/enumerable")]
        [InlineData("WeatherForecast/actionresult/ok/enumerable")]
        public async Task Get_WithEnumerableRespone_WithoutActionResult(string path)
        {
            using var http = fixture.CreateClient();
            var response = await http.GetAsync(path);
            var responseContent = await response.Content.ReadAsStringAsync();
            var jarray = JToken.Parse(responseContent);
            using (new AssertionScope())
            {
                response.Should().Be200Ok();
                jarray.Should().BeEquivalentTo(EnumerableWeatherForecastsResponse);
            }
        }

        [Theory]
        [InlineData("WeatherForecast/summary")]
        [InlineData("WeatherForecast/task/summary")]
        [InlineData("WeatherForecast/valuetask/summary")]
        [InlineData("WeatherForecast/actionresult/ok/summary")]
        public async Task Get_WithDefinedResponseTypeToBaseClass_WithoutActionResult_SummaryWeatherForecast(string path)
        {
            var expectedResponseBody = JToken.Parse(@"{""summary"":""Mild"", ""date"":""2022-11-20""}");
            using var http = fixture.CreateClient();
            var response = await http.GetAsync(path);
            var responseContent = await response.Content.ReadAsStringAsync();
            var jarray = JToken.Parse(responseContent);
            using (new AssertionScope())
            {
                response.Should().Be200Ok();
                jarray.Should().BeEquivalentTo(expectedResponseBody);
            }
        }

        [Theory]
        [InlineData("WeatherForecast/extended")]
        [InlineData("WeatherForecast/task/extended")]
        [InlineData("WeatherForecast/valuetask/extended")]
        [InlineData("WeatherForecast/actionresult/ok/extended")]
        public async Task Get_WithDefinedResponseTypeToBaseClass_WithoutActionResult_ExtendedWeatherForecast(string path)
        {
            var expectedResponseBody = JToken.Parse(@"{""summary"":""Freezing"", ""date"": ""2022-11-21"", ""TemperatureC"":-15}");
            using var http = fixture.CreateClient();
            var response = await http.GetAsync(path);
            var responseContent = await response.Content.ReadAsStringAsync();
            var jarray = JToken.Parse(responseContent);
            using (new AssertionScope())
            {
                response.Should().Be200Ok();
                jarray.Should().BeEquivalentTo(expectedResponseBody);
            }
        }
    }
}
