using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Test.Models;

namespace Dashboard.Controllers
{
    public class TrainTimesController : Controller
    {
        public HttpClient client = new HttpClient();
        public TrainTimesController()
        {
            client.BaseAddress = new Uri("http://trainlocationapi-001-site1.atempurl.com/api/");
        }

        public async Task<IActionResult> index()
        {
            var result = await client.GetAsync("Railway/GetAllRailways");
            var Railways = await result.Content.ReadAsAsync<List<Railway>>();

            SelectList railways = new SelectList(Railways, "Id", "Name");

            ViewBag.Railways = railways;

            return View();
        }

        public async Task<IActionResult> Add(int id , string msg1 ="" , string msg2 ="")
        {
            var StationResult = await client.GetAsync("Station/GetStationsInRailway/" + id);
            var Stations  = await StationResult.Content.ReadAsAsync<List<Station>>();
            SelectList stations = new SelectList(Stations, "Id", "Name");

            ViewBag.Stations = stations;

            var TrainResult = await client.GetAsync("Train/GetTrainsInRailway/" + id);
            var Trains = await TrainResult.Content.ReadAsAsync<List<Train>>();
            SelectList trains = new SelectList(Trains, "Id", "TrainNumber");

            ViewBag.Trains = trains;
            ViewBag.id = id;
            ViewBag.msg1 = msg1;
            ViewBag.msg2 = msg2;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(TrainInStationTime trainInStationTime , string hour , string minute , int id)
        {
            string t = hour + "." + minute;
            trainInStationTime.TrainTime = Double.Parse(t);

            var check = await client.GetAsync("TrainStation/GetAllStationForTrain/" + trainInStationTime.TrainId);
            var r = await check.Content.ReadAsAsync<List<TrainInStationTime>>();

            var time = r.Where(c => c.StationId.Equals(trainInStationTime.StationId)).FirstOrDefault();

            if (time is null)
            {
                var result = await client.PostAsJsonAsync("TrainStation/CreateTrainTimeForStation", trainInStationTime);


                if (result.IsSuccessStatusCode) 
                    return RedirectToAction("Add", new { id = id , msg1 = "Add Successfully ."});
                else
                    return View("Error");
            }
            else
            {
               return RedirectToAction("Add" , new {id = id , msg2 = "Already Exist !" });
            }
        }


        public async Task<IActionResult> View(int id)
        {
            var result = await client.GetAsync("TrainStation/GetAllStationForTrain/" + id);
            var trainTimes = await result.Content.ReadAsAsync<List<TrainInStationTime>>();
            ViewBag.id = id;
            return View(trainTimes);
        }

        public async Task<IActionResult> Delete(int TrainId , int StationId, int id)
        {
            var result = await client.DeleteAsync($"TrainStation/DeleteTimeForStation?TrainId={TrainId}&StationId={StationId}");

            if (result.IsSuccessStatusCode)
                return RedirectToAction("View" , new {id = id});

            else
                return View("Error");

        }

    }
}
