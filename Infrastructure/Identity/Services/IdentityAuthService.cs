using Application.Abstractions.Identity;
using Application.Dtos.Identity;
using Microsoft.AspNetCore.Identity;
using GymPortal.Infrastructure.Identity;
namespace Infrastructure.Identity.Services;

public class IdentityAuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager) : IAuthService
{
    public async Task<AuthResult> AlreadyExistsAsync(string email)
    {
        var user = await userManager.FindByEmailAsync(email);
        return user != null ? AuthResult.Failed("User already exists") : AuthResult.Ok();
    }

    public async Task<AuthResult> SignUpUserAsync(string email, string password, string? roleName = null)
    {
        var exists = await AlreadyExistsAsync(email);
        if (!exists.Succeeded)
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

    public async Task<AuthResult> SignInUserAsync(string email, string password, bool rememberMe = false)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            return AuthResult.InvalidCredentials();

        var result = await signInManager.PasswordSignInAsync(email, password, rememberMe, false);
        if (result.IsLockedOut)
            return AuthResult.LockedOut();

        if (result.IsNotAllowed)
            return AuthResult.NotAllowed();

        if (result.RequiresTwoFactor)
            return AuthResult.RequireTwoFactorAuth();

        if (!result.Succeeded)
            return AuthResult.Failed();

        return AuthResult.Ok();
    }

    public Task SignOutUserAsync() => signInManager.SignOutAsync();
}