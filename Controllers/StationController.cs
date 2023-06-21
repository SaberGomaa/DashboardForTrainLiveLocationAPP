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


        public async Task<IActionResult> Create()
        {
            try
            {
                int? x = HttpContext.Session.GetInt32("AdminId");
                if (x == null)
                {
                    return RedirectToAction("login", "operation");
                }
                var result = await client.GetAsync("Railway/GetAllRailways");
                var Railways = await result.Content.ReadAsAsync<List<Railway>>();

                SelectList railways = new SelectList(Railways, "Id", "Name");

                ViewBag.Railways = railways;

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

        public IActionResult Edit(int id)
        {
            var result = client.GetAsync("station/GetStationById?Id=" + id).Result;

            var station = result.Content.ReadAsAsync<Station>().Result;

            if (station != null)
            {
                //ViewBag.station = station;
                return View(station);
            }
            else
                return View();
        }

        [HttpPost]
        public IActionResult Edit(Station station)
        {

            var result = client.PutAsJsonAsync("station/UpdateStation?StationId="+station.Id , station).Result;

            if (result.IsSuccessStatusCode)
            {
                 return RedirectToAction("show");
            }
            return RedirectToAction("Error");
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
