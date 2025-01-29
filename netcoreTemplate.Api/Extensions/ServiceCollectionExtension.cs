using Api.Endpoints;
using Api.Middlewares;
using Application;
using Infrastructure;
using Microsoft.AspNetCore.Http.Features;
using System.Diagnostics;
using System.Reflection;
using System.Threading.RateLimiting;

namespace Api.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOpenApi();

        services.AddCors(options =>
        {
            options.AddPolicy(name: "develop", builder =>
            {
                builder//.WithOrigins("http://localhost:3000")
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            options.OnRejected = async (context, token) =>
            {
                await context.HttpContext.Response.WriteAsync("Too many request, try it later", cancellationToken: token);
            };
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                RateLimitPartition.GetConcurrencyLimiter(
                    partitionKey: "aqui el identificador",
                    factory: _ => new ConcurrencyLimiterOptions()
                    {
                        PermitLimit = 10,
                        QueueLimit = 0,
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst
                    }));
        });

        services.AddEndpoints(Assembly.GetExecutingAssembly());

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

                    });


        services.AddApplicationServices();
        services.AddInfrastructureServices(configuration);

        return services;
    }
}
