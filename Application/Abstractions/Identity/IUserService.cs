using Application.Dtos.Identity;

namespace Application.Abstractions.Identity;

public interface IUserService
{
    Task<UserResult> GetUserDetailsAsync(string userId);
    Task<UserResult> UpdateUserDetailsAsync(UserDetails userDetails);
}
