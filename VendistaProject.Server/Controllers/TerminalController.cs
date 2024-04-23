using Microsoft.AspNetCore.Mvc;

namespace VendistaProject.Server.Controllers
{
    public class TerminalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
