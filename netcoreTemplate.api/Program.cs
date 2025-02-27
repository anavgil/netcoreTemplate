// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Api.Extensions;
using Aspire.ServiceDefaults;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults(); // Aspire services

builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));

//builder.Services.AddApiServices(builder.Configuration);

builder.Services.AddApiServices(builder.Configuration,opt =>
{
    opt.UseApiVersioning = true;
    opt.UseCors = true;
    opt.UseRateLimit = false;
    opt.UseHealthChecks = false;
    opt.UseResponseCompression = false;
});

var app = builder.Build();
app.MapDefaultEndpoints(); //Aspire middleware

// Configure the HTTP request pipeline.
//app.ConfigureApplicationBuilder();
app.ConfigureApplicationBuilder(builder =>
{
    builder.UseApiVersioning = true;
    builder.UseCors = true;
    builder.UseRateLimit = false;
    builder.UseHealthChecks = false;
    builder.UseResponseCompression = false;
});

app.Run();
