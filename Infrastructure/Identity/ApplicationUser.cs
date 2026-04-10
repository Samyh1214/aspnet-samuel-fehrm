using Microsoft.AspNetCore.Identity;

namespace GymPortal.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }

    public ApplicationUser() { }

    public static ApplicationUser Create(string email)
    {
        return new ApplicationUser
        {
            UserName = email,
            Email = email,
        };
    }
}