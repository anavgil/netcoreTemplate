using Api.Endpoints;
using Api.Middlewares;
using Application;
using FastEndpoints;
using Infrastructure;
using Microsoft.AspNetCore.Http.Features;
using Scalar.AspNetCore;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddOpenApi();

builder.Services.AddFastEndpoints();

//builder.Services.RegisterJwtAuthentication(builder.Configuration);
builder.Services
        .AddExceptionHandler<CustomExceptionHandler>()
        .AddProblemDetails(options =>
            options.CustomizeProblemDetails = context =>
            {
                context.ProblemDetails.Instance =
                    $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";

                context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);

                Activity? activity = context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;
                context.ProblemDetails.Extensions.TryAdd("traceId", activity?.Id);

            }
            )
        .AddEndpoints(typeof(Program).Assembly);


builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

app.UseFastEndpoints();

app.MapDefaultEndpoints();
app.UseExceptionHandler();
app.UseRequestSecurity();
//app.MapEndpoints();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.WithDarkMode(true)
        .WithTheme(ScalarTheme.Mars)
        .WithTitle("Es un test");
    });
}

app.UseHttpsRedirection();

app.Run();
