using Microsoft.AspNetCore.Mvc;

namespace VendistaProject.UI.Controllers
{
    public class BaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
