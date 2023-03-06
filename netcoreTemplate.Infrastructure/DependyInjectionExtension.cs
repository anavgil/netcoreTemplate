using Microsoft.Extensions.DependencyInjection;
using netcoreTemplate.Domain.Interfaces;
using netcoreTemplate.Infrastructure.Repositories.Base;

namespace netcoreTemplate.Infrastructure
{
    public static class DependyInjectionExtension
    {

        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {

            services.AddHttpClient();
            services.AddScoped<IUnitOfWork,UnitOfWork>();
            return services;
        }
    }
}
