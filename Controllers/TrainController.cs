using Test.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Dashboard.Controllers
{
    public class TrainController : Controller
    {
        public HttpClient client = new HttpClient();

        public TrainController()
        {
            client.BaseAddress = new Uri("http://trainlocationapi-001-site1.atempurl.com/api/");
        }

        public ActionResult Show()
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


                return View(trains);
            }
            catch
            {
                return View("Error");
            }
        }

        public async Task<IActionResult> Create()
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

        [HttpPost]
        public ActionResult Create(Train train)
        {
            try
            {
                string time = train.TrainTimeH + "." + train.TrainTimeM;
                train.TrainTime = double.Parse(time);

                train.Conductor = "Conductor";
                train.Driver = "Driver";
                train.CurrentLocation = "CurrentLocation";
                var result = client.PostAsJsonAsync("train/createtrain", train).Result;
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

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                int? x = HttpContext.Session.GetInt32("AdminId");
                if (x == null)
                {
                    return RedirectToAction("login", "operation");
                }
                var result = client.GetAsync("train/GetTrainById?Id=" + id).Result;
                var train = result.Content.ReadAsAsync<Train>().Result;

                var res = await client.GetAsync("Railway/GetAllRailways");
                var Railways = await res.Content.ReadAsAsync<List<Railway>>();

                SelectList railways = new SelectList(Railways, "Id", "Name");
                ViewBag.Railways = railways;

                return View(train);
            }
            catch
            {
                return View("Error");

            }
        }

        [HttpPost]
        public IActionResult Edit(int id , Train train)
        {
            try
            {
                var result = client.PutAsJsonAsync("train/UpdateTrain?trainId=" + id, train).Result;

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

        public ActionResult Delete(int id)
        {
            try
            {
                int? x = HttpContext.Session.GetInt32("AdminId");
                if (x == null)
                {
                    return RedirectToAction("login", "operation");
                }
                var result = client.DeleteAsync("train/DeleteTrain?Id=" + id).Result;
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
