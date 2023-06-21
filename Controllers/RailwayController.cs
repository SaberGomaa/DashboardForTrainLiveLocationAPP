using Entites.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.Controllers
{
    public class RailwayController : Controller
    {
        public HttpClient client = new HttpClient();
        public RailwayController()
        {
            client.BaseAddress = new Uri("http://trainlocationapi-001-site1.atempurl.com/api/");
        }
      
        public async Task<IActionResult> Show()
        {
            var result = await client.GetAsync("Railway/GetAllRailways");
            var Railways = await result.Content.ReadAsAsync<List<Railway>>();

            return View(Railways);

        }

        public  async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Railway railway)
        {
            var result = await client.PostAsJsonAsync("Railway/CreateRailway" , railway);

            if(result.IsSuccessStatusCode)
            {
                return RedirectToAction("show");
            }
            else
            {
                return View("Error");
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var result = await client.GetAsync("Railway/GetAllRailways");
            var Railways = await result.Content.ReadAsAsync<List<Railway>>();
            var r = Railways.Where(c=>c.Id.Equals(id)).FirstOrDefault();
            return View(r);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Railway railway)
        {

            var result = await client.PutAsJsonAsync("Railway/UpdateRailway?id=" + id, railway);
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Show");
            }
            else 
            {
                return View("Error");
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            //var result = await client.GetAsync("Railway/GetAllRailways");
            //var Railways = await result.Content.ReadAsAsync<List<Railway>>();

            //var r = Railways.Where(c=>c.Id.Equals(id));

            var r = await client.DeleteAsync("Railway/DeleteRailway/" + id);

            if (r.IsSuccessStatusCode)
            {
                return RedirectToAction("show");
            }
            else
            {
                return View("Error");
            }

        }

    }
}
