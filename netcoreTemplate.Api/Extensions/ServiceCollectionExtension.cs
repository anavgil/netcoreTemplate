using System.Diagnostics;
using System.IO.Compression;
using System.Reflection;
using System.Threading.RateLimiting;
using Api.Endpoints;
using Api.Middlewares;
using Application;
using Asp.Versioning;
using Infrastructure;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.ResponseCompression;

namespace Api.Extensions;

/// <summary>
///
/// </summary>
public static class ServiceCollectionExtension
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddVersioning();

        services.AddOpenApi("v1", options =>
        {
            options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
        });

        services.AddOpenApi("v2");

        services.AddResposeCompression();

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

        //builder.Services.RegisterJwtAuthentication(builder.Configuration);
        services.AddExceptionHandler<GlobalExceptionHandler>()
                .AddProblemDetails(options =>
                    options.CustomizeProblemDetails = context =>
                    {
                        context.ProblemDetails.Instance =
                            $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";

                        context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);

                        Activity activity = context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;
                        context.ProblemDetails.Extensions.TryAdd("traceId", activity?.Id);
                    });

        services.AddEndpoints(Assembly.GetExecutingAssembly());

        services.AddApplicationServices();
        services.AddInfrastructureServices(configuration);

        return services;
    }

    private static IServiceCollection AddVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ApiVersionReader = ApiVersionReader.Combine(
                                        new UrlSegmentApiVersionReader(),
                                        new HeaderApiVersionReader("X-Api-Version"));
        })
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            // Replace the placeholder with the actual version
            options.SubstituteApiVersionInUrl = true;
        });
        return services;
    }

    private static IServiceCollection AddResposeCompression(this IServiceCollection services)
    {
        services.AddResponseCompression(options =>
        {
            options.EnableForHttps = true;
            options.Providers.Add<BrotliCompressionProvider>();
            options.Providers.Add<GzipCompressionProvider>();
            options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(["image/svg+xml"]);
        });

        services.Configure<BrotliCompressionProviderOptions>(options =>
        {
            options.Level = CompressionLevel.Optimal;
        });

        services.Configure<GzipCompressionProviderOptions>(options =>
        {
            options.Level = CompressionLevel.SmallestSize;
        });

        return services;
    }
}
