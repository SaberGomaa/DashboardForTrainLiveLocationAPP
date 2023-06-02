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
            int? x = HttpContext.Session.GetInt32("AdminId");
            if (x == null)
            {
                return RedirectToAction("login", "operation");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Register(Admin admin)
        {
            try
            {
                admin.Password = admin.Phone;
                admin.Email = "null";
                admin.AdminDegree = "A";
                admin.FirstTime = true;

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
            catch
            {
                return View("Error");
            }
        }

        public ActionResult view()
        {
            try
            {
                int? x = HttpContext.Session.GetInt32("AdminId");
                if (x == null)
                {
                    return RedirectToAction("login", "operation");
                }
                var result = client.GetAsync("admin/getadmins").Result;

                var a = result.Content.ReadAsAsync<List<Admin>>().Result;
                var admins = a.Where(a=>a.FirstTime == false).ToList();
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

            try
            {
                int? x = HttpContext.Session.GetInt32("AdminId");
                if (x == null)
                {
                    return RedirectToAction("login", "operation");
                }
                int id = HttpContext.Session.GetInt32("AdminId").Value;
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
            try
            {
                int? x = HttpContext.Session.GetInt32("AdminId");
                if (x == null)
                {
                    return RedirectToAction("login", "operation");
                }
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
            catch
            {
                return View("Error");
            }
        }
    }
}
