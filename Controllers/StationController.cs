using Microsoft.AspNetCore.Mvc;
using Test.Models;

namespace Dashboard.Controllers
{
    public class StationController : Controller
    {
        HttpClient client = new HttpClient();
        public StationController()
        {
            client.BaseAddress = new Uri("http://saberelsayed-001-site1.itempurl.com/api/");
        }

        [HttpGet]
        public IActionResult Show()
        {

            try
            {
                var result = client.GetAsync("station/getallstation").Result;
                
                var stations = result.Content.ReadAsAsync<IEnumerable<Station>>().Result;

                return View(stations);
            }
            catch
            {
                return RedirectToAction("Error");
            }
        }
    }
}
