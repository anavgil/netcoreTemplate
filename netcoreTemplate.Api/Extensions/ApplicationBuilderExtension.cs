using Api.Middlewares;
using FastEndpoints;

namespace Api.Extensions;

public static class ApplicationBuilderExtension
{
    public static IApplicationBuilder ConfigureApplicationBuilder(this IApplicationBuilder app)
    {
        app.UseFastEndpoints();
        app.UseExceptionHandler();
        app.UseRequestSecurity();

        app.UseStatusCodePages();
        return app;
    }
}
