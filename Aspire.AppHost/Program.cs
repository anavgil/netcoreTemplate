using System.Diagnostics;
using Aspire.AppHost;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Api>("api")
        .WithScalar();

builder.Build().Run();
