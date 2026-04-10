using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Account.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
