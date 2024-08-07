using netcoreTemplate.Api.Middlewares;
using netcoreTemplate.Api.Modules;
using netcoreTemplate.application;
using netcoreTemplate.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();
//builder.Services.RegisterJwtAuthentication(builder.Configuration);
builder.Services
        .AddExceptionHandler<GlobalExceptionHandler>()
        .AddProblemDetails()
        .AddEndpoints(typeof(Program).Assembly);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler();
app.UseRequestSecurity();
app.MapEndpoints();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
