using Microsoft.AspNetCore.Mvc;

namespace Dashboard.Controllers
{
    public class TrainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
