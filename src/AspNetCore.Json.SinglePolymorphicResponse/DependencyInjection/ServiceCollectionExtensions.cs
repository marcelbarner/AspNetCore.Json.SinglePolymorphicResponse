using AspNetCore.Json.SinglePolymorphicResponse;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSinglePolymorphicJsonResponse(this IServiceCollection services) =>
        services.AddTransient<IConfigureOptions<MvcOptions>, SinglePolymorphicJsonResponseConfigSetup>();
}
