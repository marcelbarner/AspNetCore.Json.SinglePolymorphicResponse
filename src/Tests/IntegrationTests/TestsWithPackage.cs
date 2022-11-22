using FluentAssertions.Execution;
using Newtonsoft.Json.Linq;

namespace AspNetCore.Json.SinglePolymorphicResponse.Tests.IntegrationTests
{
    public class TestsWithPackage : IClassFixture<FixtureWithPackage>
    {
        private readonly FixtureWithPackage fixture;

        public TestsWithPackage(FixtureWithPackage fixtureWithPackage)
        {
            this.fixture = fixtureWithPackage;
        }

        [Theory]
        [InlineData("WeatherForecast/enumerable")]
        [InlineData("WeatherForecast/task/enumerable")]
        [InlineData("WeatherForecast/valuetask/enumerable")]
        [InlineData("WeatherForecast/actionresult/ok/enumerable")]
        public async Task Get_WithEnumerableRespone_WithoutActionResult(string path)
        {
            var expectedResponseBody = JToken.Parse(@"[{""$type"":""summary"", ""summary"":""Mild"", ""date"":""2022-11-20""}, {""$type"":""extended"", ""summary"":""Freezing"", ""date"": ""2022-11-21"", ""TemperatureC"":-15}]");
            using var http = fixture.CreateClient();
            var response = await http.GetAsync(path);
            var responseContent = await response.Content.ReadAsStringAsync();
            using (new AssertionScope())
            {
                response.Should().Be200Ok();
                var jarray = JToken.Parse(responseContent);
                jarray.Should().BeEquivalentTo(expectedResponseBody);
            }
        }

        [Theory]
        [InlineData("WeatherForecast/summary")]
        [InlineData("WeatherForecast/task/summary")]
        [InlineData("WeatherForecast/valuetask/summary")]
        [InlineData("WeatherForecast/actionresult/ok/summary")]
        public async Task Get_WithDefinedResponseTypeToBaseClass_WithoutActionResult_SummaryWeatherForecast(string path)
        {
            var expectedResponseBody = JToken.Parse(@"{""$type"":""summary"", ""summary"":""Mild"", ""date"":""2022-11-20""}");
            using var http = fixture.CreateClient();
            var response = await http.GetAsync(path);
            var responseContent = await response.Content.ReadAsStringAsync();

            using (new AssertionScope())
            {
                response.Should().Be200Ok();
                var jarray = JToken.Parse(responseContent);
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
            var expectedResponseBody = JToken.Parse(@"{""$type"":""extended"", ""summary"":""Freezing"", ""date"": ""2022-11-21"", ""TemperatureC"":-15}");
            using var http = fixture.CreateClient();
            var response = await http.GetAsync(path);
            var responseContent = await response.Content.ReadAsStringAsync();
            using (new AssertionScope())
            {
                response.Should().Be200Ok();
                var jarray = JToken.Parse(responseContent);
                jarray.Should().BeEquivalentTo(expectedResponseBody);
            }
        }


    }
}
