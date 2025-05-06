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

        app.UseHttpsRedirection();
        //Customs middleware
        app.UseMiddleware<ValidationExceptionMiddleware>();
        app.UseRequestSecurity();

        // Returns the Problem Details response for (empty) non-successful responses
        app.UseStatusCodePages();

        app.UseAuthentication();
        app.UseAuthorization();

        return app;
    }


    public static IApplicationBuilder ConfigureApplicationBuilder(this IApplicationBuilder app, Action<ConfigurationBuilder> builder)
    {
        ConfigurationBuilder settings = new();

        builder?.Invoke(settings);

        app.ConfigureApplicationBuilder(settings);

        return app;
    }

    private static IApplicationBuilder ConfigureApplicationBuilder(this IApplicationBuilder app,ConfigurationBuilder builder)
    {
        app.UseSerilogRequestLogging();

        app.UseExceptionHandler();

        if (builder.UseRateLimit)
            app.UseRateLimiter();

        app.UseHttpsRedirection();

        if (builder.UseResponseCompression)
            app.UseResponseCompression();

        // Returns the Problem Details response for (empty) non-successful responses
        app.UseStatusCodePages();

        if (app is WebApplication webApp)
        {
            webApp.MapEndpoints();
            webApp.MapOpenApi();

            if (webApp.Environment.IsDevelopment())
            {
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

        app.UseRouting();

        if (builder.UseCors)
            app.UseCors("develop");

        app.UseAuthentication();
        app.UseAuthorization();

        //Customs middleware
        app.UseMiddleware<ValidationExceptionMiddleware>();

        return app;
    }
}
