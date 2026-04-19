using Application.Abstractions.Identity;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Presentation.Areas.Authentication.Models;

namespace Presentation.Areas.Authentication.Controllers;

[Area("Authentication")]
[Route("registration")]
public class SignUpController(IAuthService authService) : Controller
{
    #region get-started

    [HttpGet("get-started")]
    public IActionResult GetStarted()
    {
        var redirectPath = AuthenticationRedirectManager.GetRedirectPath(User, _redirectPaths);
        return !string.IsNullOrWhiteSpace(redirectPath)
            ? Redirect(redirectPath)
            : View();
    }

    [HttpPost("get-started")]
    [ValidateAntiForgeryToken]
    public IActionResult GetStarted(GetStartedForm form)
    {
        if (!ModelState.IsValid)
            return View(form);

        TempData["Email"] = form.Email;
        return RedirectToAction(nameof(SetPassword));
    }

    #endregion

    #region set-password

    [HttpGet("set-password")]
    public IActionResult SetPassword()
    {
        if (TempData.Peek("Email") == null)
            return RedirectToAction(nameof(GetStarted));

        return View();
    }

    [HttpPost("set-password")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SetPassword(SetPasswordForm form)
    {
        if (!ModelState.IsValid)
            return View(form);

        var email = TempData["Email"]?.ToString();
        if (string.IsNullOrWhiteSpace(email))
            return RedirectToAction(nameof(GetStarted));

        var result = await authService.SignUpUserAsync(email, form.Password);

        if (result.Succeeded)
            return Redirect("/sign-in");

        TempData["ErrorMessage"] = "Något gick fel, försök igen.";
        return View(form);
    }

    #endregion

    private readonly Dictionary<string, string> _redirectPaths = new()
    {
        { "Admin", "/admin" },
        { "Member", "/me" }
    };
}