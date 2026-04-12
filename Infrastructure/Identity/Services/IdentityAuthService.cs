using Application.Abstractions.Identity;
using Application.Dtos.Results;
using GymPortal.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Services;

public class IdentityAuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager) : IAuthService
{
    public async Task<AuthResult> SignInUserAsync(string email, string password, bool rememberMe = false)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            return AuthResult.Failed("Incorrect email address or password");

        var result = await signInManager.PasswordSignInAsync(email, password, rememberMe, false);
        if (result.IsLockedOut)
            return AuthResult.Failed("This user is temporary locked out");

        if (result.IsNotAllowed)
            return AuthResult.Failed("This user is not allowed to login");

        if (result.RequiresTwoFactor)
            return AuthResult.Failed("This user requires two-factor authentication");

        if (!result.Succeeded)
            return AuthResult.Failed("Incorrect email address or password");

        return AuthResult.Ok();
    }
    public async Task<AuthResult> SignUpUserAsync(string email, string password, string? roleName = null)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            return AuthResult.Failed("Email and password are required");

        var existingUser = await userManager.FindByEmailAsync(email);
        if (existingUser != null)
            return AuthResult.Failed("User already exists");

        var user = ApplicationUser.Create(email);
        user.EmailConfirmed = true;

        var result = await userManager.CreateAsync(user, password);
        if (!result.Succeeded)
            return AuthResult.Failed(result.Errors.FirstOrDefault()?.Description);

        var role = roleName ?? "Member";
        if (await roleManager.RoleExistsAsync(role))
            await userManager.AddToRoleAsync(user, role);

        return AuthResult.Ok();
    }

    public Task SignOutUserAsync() => signInManager.SignOutAsync();
}