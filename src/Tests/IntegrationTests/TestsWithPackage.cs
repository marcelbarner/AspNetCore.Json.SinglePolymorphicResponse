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

        [Fact]
        public async Task Get_WithEnumerableRespone_WithoutActionResult()
        {
            var expectedResponseBody = JToken.Parse(@"[{""$type"":""summary"", ""summary"":""Mild"", ""date"":""2022-11-20""}, {""$type"":""extended"", ""summary"":""Freezing"", ""date"": ""2022-11-21"", ""TemperatureC"":-15}]");
            using var http = fixture.CreateClient();
            var response = await http.GetAsync("WeatherForecast");
            var responseContent = await response.Content.ReadAsStringAsync();
            var jarray = JToken.Parse(responseContent);
            using (new AssertionScope())
            {
                response.Should().Be200Ok();
                jarray.Should().BeEquivalentTo(expectedResponseBody);
            }
        }

        [Fact]
        public async Task Get_WithDefinedResponseTypeToBaseClass_WithoutActionResult_SummaryWeatherForecast()
        {
            var expectedResponseBody = JToken.Parse(@"{""$type"":""summary"", ""summary"":""Mild"", ""date"":""2022-11-20""}");
            using var http = fixture.CreateClient();
            var response = await http.GetAsync("WeatherForecast/summary");
            var responseContent = await response.Content.ReadAsStringAsync();
            var jarray = JToken.Parse(responseContent);
            using (new AssertionScope())
            {
                response.Should().Be200Ok();
                jarray.Should().BeEquivalentTo(expectedResponseBody);
            }
        }

        [Fact]
        public async Task Get_WithDefinedResponseTypeToBaseClass_WithoutActionResult_ExtendedWeatherForecast()
        {
            var expectedResponseBody = JToken.Parse(@"{""$type"":""extended"", ""summary"":""Freezing"", ""date"": ""2022-11-21"", ""TemperatureC"":-15}");
            using var http = fixture.CreateClient();
            var response = await http.GetAsync("WeatherForecast/extended");
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
