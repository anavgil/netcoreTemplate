using Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults(); // Aspire services

builder.Services.AddApiServices(builder.Configuration);

var app = builder.Build();
app.MapDefaultEndpoints(); //Aspire middleware

// Configure the HTTP request pipeline.
app.ConfigureApplicationBuilder();

app.UseHttpsRedirection();

app.Run();
