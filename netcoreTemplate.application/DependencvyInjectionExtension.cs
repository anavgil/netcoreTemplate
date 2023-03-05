using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace netcoreTemplate.application;

public static class DependencvyInjectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(conf=> {
            conf.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            }
        );
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }
}
