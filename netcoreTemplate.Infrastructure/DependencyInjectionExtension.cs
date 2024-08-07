using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using netcoreTemplate.Domain.Interfaces;
using netcoreTemplate.Infrastructure.Persistence.Identity;
using netcoreTemplate.Infrastructure.Persistence.Identity.Model;
using netcoreTemplate.Infrastructure.Repositories.Base;
using System.Reflection;

namespace netcoreTemplate.Infrastructure
{
    public static class DependencyInjectionExtension
    {

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSqlite<IdentityContext>(configuration.GetConnectionString("Default"), options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            })
            .AddIdentityCore<User>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<IdentityContext>();

            services.AddHttpClient();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
