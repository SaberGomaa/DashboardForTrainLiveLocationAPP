using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Test.Models;

namespace Dashboard.Controllers
{
    public class TrainController : Controller
    {
        public HttpClient client = new HttpClient();

        public TrainController()
        {
            client.BaseAddress = new Uri("http://saberelsayed-001-site1.itempurl.com/api/");
        }

        public ActionResult Show()
        {
            try
            {
                var result = client.GetAsync("Train/GetTrains").Result;
                var trains = result.Content.ReadAsAsync<List<Train>>().Result;
                return View(trains);
            }
            catch
            {
                return View("Error");
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Train train)
        {
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

        public IActionResult Edit (int id)
        {
            var result = client.GetAsync("train/GetTrainById?Id="+id).Result;
            var train = result.Content.ReadAsAsync<Train>().Result;

            return View(train);
        }

        [HttpPost]
        public IActionResult Edit(int id , Train train)
        {

            var result = client.PutAsJsonAsync("train/UpdateTrain?trainId="+id, train).Result;

            if(result.IsSuccessStatusCode)
            {
                return RedirectToAction("show");
            }
            else
            {
                return View("Error");

            }
        }

        public ActionResult Delete(int id)
        {
            var result = client.DeleteAsync("train/DeleteTrain?Id="+id).Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("show");
            }
            return View("Error");
        }

    }
}
