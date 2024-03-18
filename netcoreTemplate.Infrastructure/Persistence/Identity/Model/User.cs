using Microsoft.AspNetCore.Identity;

namespace netcoreTemplate.Infrastructure.Persistence.Identity.Model
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
    }
}
