using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("error")]
public class ErrorController : Controller
{
    [HttpGet("{statusCode}")]
    public IActionResult Index(int statusCode)
    {
        if (statusCode == 404)
            return View("Index");

        return View("Index");
    }
}