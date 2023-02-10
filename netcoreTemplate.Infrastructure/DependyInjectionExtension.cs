using Microsoft.Extensions.DependencyInjection;

namespace netcoreTemplate.Infrastructure
{
    public static class DependyInjectionExtension
    {

        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {

            services.AddHttpClient();
            return services;
        }
    }
}
