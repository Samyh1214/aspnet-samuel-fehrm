namespace Application.Dtos.Identity;

public enum AuthErrorType
{
    None,
    InvalidCredentials,
    RequireTwoFactorAuth,
    LockedOut,
    NotAllowed,
    Error
}