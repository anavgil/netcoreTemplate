using Api.Endpoints;
using Api.Middlewares;
using Scalar.AspNetCore;
using Serilog;

namespace Api.Extensions;

/// <summary>
///
/// </summary>
public static class ApplicationBuilderExtension
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder ConfigureApplicationBuilder(this IApplicationBuilder app)
    {
        app.UseExceptionHandler();
        app.UseHttpsRedirection();
        app.UseSerilogRequestLogging();

        app.UseRateLimiter();
        app.UseResponseCompression();

        if (app is WebApplication webApp)
        {
            if (webApp.Environment.IsDevelopment())
            {
                app.UseCors("develop");
                webApp.MapEndpoints();
                webApp.MapOpenApi();

                webApp.MapScalarApiReference(option =>
                {
                    option.Title = "API Reference";
                    option.Theme = ScalarTheme.Mars;
                    option.Layout = ScalarLayout.Modern;
                    option.Favicon = "path";
                    //option.WithHttpBearerAuthentication(jwt =>
                    //{
                    //    jwt.Token = "";
                    //});

                    //option.Authentication = new ScalarAuthenticationOptions
                    //{
                    //    PreferredSecurityScheme = "Bearer",
                    //};
                });
            }
        }

        app.UseAuthentication();
        app.UseAuthorization();

        //Customs middleware
        app.UseMiddleware<ValidationExceptionMiddleware>();
        app.UseRequestSecurity();

        // Returns the Problem Details response for (empty) non-successful responses
        app.UseStatusCodePages();

        return app;
    }
}
