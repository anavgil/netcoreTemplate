using Api.Extensions;
using Asp.Versioning.ApiExplorer;

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
    //app.MapScalarApiReference(options =>
    //{
    //    options.WithDarkMode(true)
    //    .WithTheme(ScalarTheme.Mars)
    //    .WithTitle("Es un test");
    //});
    var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        //c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger UI Personalized .Net 9");
        //c.RoutePrefix = string.Empty;

        var apiVersionDescriptions = app.DescribeApiVersions();
        foreach (var apiVersionDescription in apiVersionDescriptions)
        {
            var url = $"/swagger/{apiVersionDescription.GroupName}/swagger.json";
            var name = apiVersionDescription.GroupName.ToUpperInvariant();

            c.SwaggerEndpoint(url, name);
        }

        //foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        //{
        //    c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
        //        description.GroupName.ToUpperInvariant());
        //}
    });

    app.UseCors("dev");
}

app.UseHttpsRedirection();

app.Run();
