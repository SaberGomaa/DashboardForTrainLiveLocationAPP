using Microsoft.AspNetCore.Mvc;
using Test.Models;

namespace Dashboard.Controllers
{
    public class UserController : Controller
    {
        public HttpClient client = new HttpClient();
        public UserController()
        {
            client.BaseAddress = new Uri("http://saberelsayed-001-site1.itempurl.com/api/");
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }


        public new ActionResult Profile(int id)
        {
            try
            {
                var result = client.GetAsync("user/getuserbyid?Id=" + id).Result;

                var user = result.Content.ReadAsAsync<User>().Result;

                return View(user);
            }
            catch
            {
                return View("Error");
            }
        }

        public ActionResult Show()
        {
            try
            {
                var result = client.GetAsync("user/getusers").Result;

                var users = result.Content.ReadAsAsync<List<User>>().Result;

                return View(users);
            }
            catch { return View("Error"); }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                var result = client.DeleteAsync("user/DeleteUser?Id=" + id).Result;

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
