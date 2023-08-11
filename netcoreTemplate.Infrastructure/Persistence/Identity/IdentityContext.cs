using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using netcoreTemplate.Infrastructure.Persistence.Identity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netcoreTemplate.Infrastructure.Persistence.Identity
{
    public class IdentityContext: IdentityDbContext<User>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options):base(options)
        {
            
        }
    }
}
