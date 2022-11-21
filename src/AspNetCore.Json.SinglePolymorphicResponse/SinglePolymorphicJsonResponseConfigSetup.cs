using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AspNetCore.Json.SinglePolymorphicResponse
{
    internal class SinglePolymorphicJsonResponseConfigSetup : IConfigureOptions<MvcOptions>
    {
        public SinglePolymorphicJsonResponseConfigSetup(IOptions<JsonOptions> jsonOptions)
        {
            JsonOptions = jsonOptions;
        }

        public IOptions<JsonOptions> JsonOptions { get; }

        public void Configure(MvcOptions options)
        {
            var formatter = SystemTextJsonOutputFormatter.CreateFormatter(JsonOptions.Value);
            options.OutputFormatters.Insert(0, formatter);
        }
    }
}
