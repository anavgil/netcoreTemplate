using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using netcoreTemplate.Infrastructure.Persistence.Identity.Model;

namespace netcoreTemplate.Infrastructure.Persistence.Identity;

public class IdentityContext(DbContextOptions<IdentityContext> options) : IdentityDbContext<User>(options)
{
}
