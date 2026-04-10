using Microsoft.AspNetCore.Mvc;
using Presentation.Areas.Authentication.Models;

namespace Presentation.WebApp.Areas.Authentication.Controllers;

[Area("Authentication")]
[Route("registration")]
public class SignUpController : Controller
{
    #region get-started

    [HttpGet("get-started")]
    public IActionResult GetStarted()
    {
        return View();
    }

    [HttpPost("get-started")]
    public IActionResult GetStarted(GetStartedForm form)
    {
        return View();
    }

    #endregion

    #region set-password

    [HttpGet("set-password")]
    public IActionResult SetPassword()
    {
        return View();
    }

    [HttpPost("set-password")]
    public IActionResult SetPassword(SetPasswordForm form)
    {
        return View();
    }

    #endregion

}
