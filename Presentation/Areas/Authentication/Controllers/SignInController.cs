using Application.Abstractions.Identity;
using Application.Dtos.Results;
using Application.Services;
using GymPortal.Infrastructure.Identity;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Areas.Authentication.Models;



namespace Presentation.Areas.Authentication.Controllers;

[Area("Authentication")]
public class SignInController(SignInManager<ApplicationUser> signInManager) : Controller
{
    private readonly Dictionary<string, string> _redirectPaths = new()
    {
        { "Admin", "/admin" },
        { "Member", "/me" }
    };

    #region local sign-in

    [HttpGet("sign-in")]
    public IActionResult SignIn(string? returnUrl = null)
    {
        ViewBag.ReturnUrl = returnUrl;
        var redirectPath = AuthenticationRedirectManager.GetRedirectPath(User, _redirectPaths);

        return !string.IsNullOrWhiteSpace(redirectPath)
            ? Redirect(redirectPath)
            : View();
    }

    [HttpPost("sign-in")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SignIn(SignInForm form, string? returnUrl = null)
    {
        ViewBag.ReturnUrl = returnUrl;

        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = "Incorrect email address or password";
            return View(form);
        }

        var result = await signInManager.PasswordSignInAsync(form.Email, form.Password, form.RememberMe, false);

        if (result.IsLockedOut)
        {
            TempData["ErrorMessage"] = "This user is temporary locked out";
            return View(form);
        }

        if (result.IsNotAllowed)
        {
            TempData["ErrorMessage"] = "This user is not allowed to login";
            return View(form);
        }

        if (result.RequiresTwoFactor)
        {
            TempData["ErrorMessage"] = "This user requires two-factor authentication";
            return View(form);
        }

        if (!result.Succeeded)
        {
            TempData["ErrorMessage"] = "Incorrect email address or password";
            return View(form);
        }

        if (!string.IsNullOrWhiteSpace(returnUrl))
            return Redirect(returnUrl);

        var redirectPath = AuthenticationRedirectManager.GetRedirectPath(User, _redirectPaths);
        return !string.IsNullOrWhiteSpace(redirectPath)
            ? Redirect(redirectPath)
            : Redirect("/");
    }

    #endregion


    [HttpPost("external-login")]
    [ValidateAntiForgeryToken]
    public IActionResult ExternalLogin(string provider, string? returnUrl = null)
    {
        var callbackUrl = Url.Action(nameof(ExternalLoginCallback), "SignIn", new { area = "Authentication", returnUrl });
        var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, callbackUrl);

        return Challenge(properties, provider);
    }

    [HttpGet]
    public IActionResult ExternalLoginCallback(string? returnUrl = null, string? remoteError = null)
    {
        if (!string.IsNullOrWhiteSpace(remoteError))
        {
            TempData["ErrorMessage"] = $"External provider error: {remoteError}";
            return RedirectToAction(nameof(SignIn), new { returnUrl });
        }

        return RedirectToAction(nameof(SignIn), new { returnUrl });
    }

}
