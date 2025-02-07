using Microsoft.AspNetCore.Identity;

namespace Domain.Identity.Model;

public class User : IdentityUser
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public int Age { get; set; } = default!;
}
