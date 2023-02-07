using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace netcoreTemplate.application;

public static class DependencvyInjectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services){

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}
