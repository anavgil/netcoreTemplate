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
        //app.UseFastEndpoints();
        app.UseExceptionHandler();
        app.UseRequestSecurity();

        return app;
    }
}
