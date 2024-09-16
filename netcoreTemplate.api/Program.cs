using Api.Endpoints;
using Api.Middlewares;
using Application;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

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

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

app.MapDefaultEndpoints();

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
