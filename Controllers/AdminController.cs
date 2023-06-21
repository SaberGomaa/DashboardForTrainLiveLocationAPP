using Microsoft.AspNetCore.Mvc;
using Test.Models;

namespace Dashboard.Controllers
{
    public class AdminController : Controller
    {
        public HttpClient client = new HttpClient();
        public AdminController()
        {
            client.BaseAddress = new Uri("http://trainlocationapi-001-site1.atempurl.com/api/");
        }

      

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdminCreate admin)
        {
            using var stream = admin.image.OpenReadStream();
            using var client = new HttpClient();
            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(admin.Email), "Email");
            content.Add(new StringContent(admin.Name), "Name");
            content.Add(new StringContent(admin.Password), "Password");
            content.Add(new StringContent(admin.AdminDegree), "AdminDegree");
            content.Add(new StringContent(admin.Phone), "Phone");
            content.Add(new StringContent(admin.FirstTime.ToString()), "FirstTime");
            content.Add(new StreamContent(stream), "image", admin.image.FileName);

            var response = await client.PostAsync("http://trainlocationapi-001-site1.atempurl.com/api/Admin/CreateAdmin", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("view");
            }
            else
            {
                return View("Error");
            }
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
