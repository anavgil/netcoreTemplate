using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Persistence.Identity.Model;

public class User : IdentityUser
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
}
