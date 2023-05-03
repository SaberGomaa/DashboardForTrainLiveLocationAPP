using Microsoft.AspNetCore.Mvc;
using Test.Models;

namespace Dashboard.Controllers
{
    public class AdminController : Controller
    {
        public HttpClient client = new HttpClient();
        public AdminController()
        {
            client.BaseAddress = new Uri("http://saberelsayed-001-site1.itempurl.com/api/");
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Admin admin)
        {
            admin.Id = 0;
            var result = client.PostAsJsonAsync("admin/createadmin", admin).Result;

            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("view");
            }
            else
            {
                return View("Error");

            }
        }

        public ActionResult view()
        {
            try
            {
                var result = client.GetAsync("admin/getadmins").Result;

                var admins = result.Content.ReadAsAsync<List<Admin>>().Result;

                if (admins == null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(admins);
                }
            }
            catch
            {
                return View("Error");
            }
        }

        public new ActionResult Profile()
        {

            int id = HttpContext.Session.GetInt32("AdminId").Value;

            try
            {
                var result = client.GetAsync("admin/getadmin/"+id).Result;

                var admin = result.Content.ReadAsAsync<Admin>().Result;

                if (admin == null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(admin);
                }
            }
            catch
            {
                return View("Error");
            }
        }

        public ActionResult Delete(int id)
        {
            var deleteAdmin = client.DeleteAsync("admin/DeleteAdmin/" + id).Result;

            if (deleteAdmin.IsSuccessStatusCode)
            {
                return RedirectToAction("view");
            }
            else
            {
                return View("Error");
            }
        }


    }

}
