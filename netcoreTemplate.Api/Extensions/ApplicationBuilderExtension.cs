using Api.Endpoints;
using Api.Middlewares;

namespace Api.Extensions;

public static class ApplicationBuilderExtension
{
    public static IApplicationBuilder ConfigureApplicationBuilder(this IApplicationBuilder app)
    {
        if (app is WebApplication webApp)
        {
            webApp.MapEndpoints();
        }

        app.UseRateLimiter();
        app.UseExceptionHandler();
        app.UseRequestSecurity();
        app.UseCors("develop");

        // Returns the Problem Details response for (empty) non-successful responses
        app.UseStatusCodePages();

        return app;
    }
}
