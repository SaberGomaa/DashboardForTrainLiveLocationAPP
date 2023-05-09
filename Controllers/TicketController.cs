using Microsoft.AspNetCore.Mvc;

namespace Dashboard.Controllers
{
    public class TicketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
