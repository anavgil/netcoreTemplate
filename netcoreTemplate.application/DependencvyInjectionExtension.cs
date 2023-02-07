using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace netcoreTemplate.application;

public static class DependencvyInjectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services){

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        return services;
    }
}
