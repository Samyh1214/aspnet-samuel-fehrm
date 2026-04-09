using Application.Dtos.Identity;

namespace Application.Abstractions.Identity;

public interface IAuthService
{
    Task<AuthResult> SignUpUserAsync(string email, string password, string? roleName = null);
    Task<AuthResult> SignInUserAsync(string email, string password, bool rememberMe = false);
    Task<AuthResult> AlreadyExistsAsync(string email);
    Task SignOutUserAsync();
}
