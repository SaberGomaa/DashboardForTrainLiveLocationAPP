using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Test.Models;

namespace Dashboard.Controllers
{
    public class StationController : Controller
    {
        HttpClient client = new HttpClient();
        public StationController()
        {
            client.BaseAddress = new Uri("http://trainlocationapi-001-site1.atempurl.com/api/");
        }

        [HttpGet]
        public IActionResult Show()
        {

            try
            {
                int? x = HttpContext.Session.GetInt32("AdminId");
                if (x == null)
                {
                    return RedirectToAction("login" , "operation");
                }
                var result = client.GetAsync("station/getallstation").Result;
                
                var stations = result.Content.ReadAsAsync<IEnumerable<Station>>().Result;

                return View(stations);
            }
            catch
            {
                return View("Error");
            }
        }


        public ActionResult Create()
        {
            try
            {
                int? x = HttpContext.Session.GetInt32("AdminId");
                if (x == null)
                {
                    return RedirectToAction("login", "operation");
                }
                var result = client.GetAsync("Train/GetTrains").Result;
                var trains = result.Content.ReadAsAsync<List<Train>>().Result;

                List<int> selTrains = trains.Select(x => x.Id).ToList();

                ViewBag.Trains = selTrains;

                return View();
            }
            catch
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Create(Station station)
        {
            try
            {

                var result = client.PostAsJsonAsync("station/createstation", station).Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Show");
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View("Error");
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                int? x = HttpContext.Session.GetInt32("AdminId");
                if (x == null)
                {
                    return RedirectToAction("login", "operation");
                }
                var result = client.DeleteAsync("station/DeleteStation?Id="+id).Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("show");
                }
                else
                {
                    return View("Error");
                }
            }
            catch
            {
                return View("Error");
            }
        }

    }
}
