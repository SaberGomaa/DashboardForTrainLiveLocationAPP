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

        public async Task<IActionResult> Add(int id)
        {
            return View();
        }


        public async Task<IActionResult> View(int id)
        {
            return View();
        }
    }
}
