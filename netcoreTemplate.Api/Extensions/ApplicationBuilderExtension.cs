using Api.Middlewares;
using FastEndpoints;
using Api.Endpoints;

namespace Api.Extensions;

public static class ApplicationBuilderExtension
{
    public static IApplicationBuilder ConfigureApplicationBuilder(this IApplicationBuilder app)
    {
        if (app is WebApplication webApp)
        {
            webApp.MapEndpoints();
        }
        app.UseExceptionHandler();
        app.UseRequestSecurity();

        // Returns the Problem Details response for (empty) non-successful responses
        app.UseStatusCodePages();

        return app;
    }
}
