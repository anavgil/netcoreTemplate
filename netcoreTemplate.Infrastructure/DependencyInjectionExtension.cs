using Domain.Interfaces;
using Infrastructure.Persistence.Identity;
using Infrastructure.Persistence.Identity.Model;
using Infrastructure.Repositories.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Infrastructure
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
            services.AddScoped<IUnitOfWork, UnitOfWork<IdentityContext>>();

            return services;
        }
    }
}
