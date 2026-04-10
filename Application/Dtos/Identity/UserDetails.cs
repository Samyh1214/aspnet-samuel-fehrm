namespace Application.Dtos.Identity;

public sealed record UserDetails
(
    string UserId,
    string? Email,
    string? FirstName,
    string? LastName,
    string? PhoneNumber,
    string? ImageUrl
);