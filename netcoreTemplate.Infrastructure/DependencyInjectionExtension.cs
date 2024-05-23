using EntityFrameworkCore.UnitOfWork.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using netcoreTemplate.Infrastructure.Persistence.Identity;
using netcoreTemplate.Infrastructure.Persistence.Identity.Model;

using System.Reflection;

namespace netcoreTemplate.Infrastructure;

public static class DependencyInjectionExtension
{

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSqlite<IdentityContext>(configuration.GetConnectionString("Default"), options =>
        {
            options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
        })
        .AddIdentityCore<User>()
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<IdentityContext>();

        services.AddHttpClient();
        services.AddUnitOfWork<IdentityContext>();
        //services.AddUnitOfWork<ApplicationContext>();

        return services;
    }
}
