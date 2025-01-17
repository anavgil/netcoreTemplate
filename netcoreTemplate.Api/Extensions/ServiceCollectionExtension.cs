using Api.Middlewares;
using FastEndpoints;
using Microsoft.AspNetCore.Http.Features;
using System.Diagnostics;
using Application;
using Api.Endpoints;
using Infrastructure;

namespace Api.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApiServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddOpenApi();

        services.AddFastEndpoints();

        //builder.Services.RegisterJwtAuthentication(builder.Configuration);
        services.AddExceptionHandler<CustomExceptionHandler>()
                .AddProblemDetails(options =>
                    options.CustomizeProblemDetails = context =>
                    {
                        context.ProblemDetails.Instance =
                            $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";

                        context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);

                        Activity activity = context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;
                        context.ProblemDetails.Extensions.TryAdd("traceId", activity?.Id);

                    }
                    )
                .AddEndpoints(typeof(Program).Assembly);


        services.AddApplicationServices();
        services.AddInfrastructureServices(configuration);

        return services;
    }
}
