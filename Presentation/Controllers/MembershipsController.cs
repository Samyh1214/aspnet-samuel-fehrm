using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("memberships")]
public class MembershipsController : Controller
{
    [HttpGet("")]
    [AllowAnonymous]
    public IActionResult Index()
    {
        ViewData["Title"] = "Our Memberships";
        return View();
    }
}
