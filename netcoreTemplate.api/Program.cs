using Api.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults(); // Aspire services

builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));

builder.Services.AddApiServices(builder.Configuration);

var app = builder.Build();
app.MapDefaultEndpoints(); //Aspire middleware

// Configure the HTTP request pipeline.
app.ConfigureApplicationBuilder();

app.UseHttpsRedirection();

app.Run();
