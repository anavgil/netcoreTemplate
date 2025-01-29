using Api.Extensions;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults(); // Aspire services

builder.Services.AddApiServices(builder.Configuration);

var app = builder.Build();
app.MapDefaultEndpoints(); //Aspire middleware

app.ConfigureApplicationBuilder();

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

    app.UseCors("dev");
}

app.UseHttpsRedirection();

app.Run();
