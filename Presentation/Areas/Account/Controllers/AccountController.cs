
using Application.Abstractions.Identity;
using Application.Dtos.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Areas.Account.Models;
using System.Security.Claims;

namespace Presentation.Areas.Account.Controllers;

[Area("Account")]
[Route("me")]
[Authorize]
public class AccountController(IAuthService authService, IAccountService accountService) : Controller
{
    public IActionResult Index() => RedirectToAction(nameof(AboutMe));

    [HttpGet("about-me")]
    public async Task<IActionResult> AboutMe()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!string.IsNullOrWhiteSpace(userId))
        {
            var account = await accountService.GetUserAccountAsync(userId);
            var viewModel = new AboutMeViewModel
            {
                AboutMeForm = new AboutMeForm
                {
                    FirstName = account.Details?.FirstName ?? "",
                    LastName = account.Details?.LastName ?? "",
                    Email = account.Details?.Email ?? "",
                    PhoneNumber = account.Details?.PhoneNumber ?? ""
                },
                ProfileImageUrl = account.Details?.ImageUrl ?? "~/images/profile-image-avatar.png"
            };

            return View(viewModel);
        }

        await authService.SignOutUserAsync();
        return Redirect("/");
    }

    [HttpPost("about-me")]
    public async Task<IActionResult> AboutMe(AboutMeViewModel viewModel)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(userId))
            return RedirectToAction(nameof(SignOut));

        var account = await accountService.GetUserAccountAsync(userId);
        if (account.Details is null)
            return RedirectToAction(nameof(SignOut));

        viewModel.ProfileImageUrl = account.Details.ImageUrl ?? "~/images/profile-image-avatar.png";

        if (!ModelState.IsValid)
            return View(viewModel);

        var imageUrl = account.Details.ImageUrl;

        if (viewModel.AboutMeForm.ProfileImage is not null && viewModel.AboutMeForm.ProfileImage.Length > 0)
        {
            imageUrl = await SaveProfileImageAsync(viewModel.AboutMeForm.ProfileImage);
        }

        var details = new UpdateAccountDetails(
            userId,
            viewModel.AboutMeForm.Email,
            viewModel.AboutMeForm.FirstName,
            viewModel.AboutMeForm.LastName,
            viewModel.AboutMeForm.PhoneNumber,
            imageUrl
        );

        var result = await accountService.UpdateUserAccountDetailsAsync(details);
        if (!result.Succeeded)
        {
            viewModel.ProfileImageUrl = imageUrl ?? "~/images/profile-image-avatar.png";
            viewModel.Message = "Unable to save changes";
            return View(viewModel);
        }

        return RedirectToAction(nameof(AboutMe));
    }

    [HttpGet("sign-out")]
    public new async Task<IActionResult> SignOut()
    {
        await authService.SignOutUserAsync();
        return Redirect("/");
    }

    [HttpGet("remove-account")]
    public async Task<IActionResult> RemoveAccount()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!string.IsNullOrWhiteSpace(userId))
        {
            var deleted = await accountService.DeleteUserAccountAsync(userId);
            if (!deleted.Succeeded)
            {
                ViewBag.Message = deleted.ErrorMessage;
                return View();
            }
        }

        await authService.SignOutUserAsync();
        return Redirect("/");
    }


    private static async Task<string> SaveProfileImageAsync(IFormFile file)
    {
        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "profiles");
        Directory.CreateDirectory(uploadsFolder);

        var extension = Path.GetExtension(file.FileName);
        var fileName = $"{Guid.NewGuid()}{extension}";
        var filePath = Path.Combine(uploadsFolder, fileName);

        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        return $"/uploads/profiles/{fileName}";
    }
}
