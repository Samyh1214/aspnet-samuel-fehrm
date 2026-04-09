using System.ComponentModel.DataAnnotations;

namespace Presentation.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Förnamn krävs")]
    [Display(Name = "Förnamn")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Efternamn krävs")]
    [Display(Name = "Efternamn")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "E-post krävs")]
    [EmailAddress(ErrorMessage = "Ogiltig e-postadress")]
    [Display(Name = "E-post")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Lösenord krävs")]
    [MinLength(8, ErrorMessage = "Lösenordet måste vara minst 8 tecken")]
    [DataType(DataType.Password)]
    [Display(Name = "Lösenord")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Bekräfta lösenord krävs")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Lösenorden matchar inte")]
    [Display(Name = "Bekräfta lösenord")]
    public string ConfirmPassword { get; set; } = string.Empty;
}