using Api.Endpoints;
using Api.Middlewares;
using Asp.Versioning.ApiExplorer;

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

        app.UseRateLimiter();
        app.UseExceptionHandler();
        app.UseRequestSecurity();
        app.UseCors("develop");

        // Returns the Problem Details response for (empty) non-successful responses
        app.UseStatusCodePages();

        return app;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder app)
    {
        if (app is WebApplication webApp)
        {
            webApp.MapEndpoints();

            var apiVersionDescriptionProvider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                var apiVersionDescriptions = webApp.DescribeApiVersions();
                foreach (var apiVersionDescription in apiVersionDescriptions)
                {
                    var url = $"/swagger/{apiVersionDescription.GroupName}/swagger.json";
                    var name = apiVersionDescription.GroupName.ToUpperInvariant();

                    options.SwaggerEndpoint(url, name);
                }
            });
        }

        return app;
    }
}
