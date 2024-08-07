using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using netcoreTemplate.Application.Test.Service;
using System.Reflection;

namespace netcoreTemplate.application;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {


        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddBusinessLogicServices();

        return services;
    }

    private static IServiceCollection AddBusinessLogicServices(this IServiceCollection services)
    {
        services.AddTransient<ITestService, TestService>();

        return services;
    }
}
