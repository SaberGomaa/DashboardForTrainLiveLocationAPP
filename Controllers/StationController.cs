using Microsoft.AspNetCore.Mvc;

namespace Dashboard.Controllers
{
    public class StationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
