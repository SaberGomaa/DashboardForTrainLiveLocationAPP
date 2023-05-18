using Dashboard.Models;
using Microsoft.AspNetCore.Mvc;
using Test.Models;

namespace Dashboard.Controllers
{
    public class OperationController : Controller
    {
        public HttpClient client = new HttpClient();
        public OperationController()
        {
            client.BaseAddress = new Uri("http://saberelsayed-001-site1.itempurl.com/api/");
        }
        // GET: Operation
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            //if (Request.Cookies["logindata"] != null)
            //{
            //    Session["Admin"] = Request.Cookies["logindata"].Values["Admin"].ToString();
            //    return RedirectToAction("show", "user");
            //}
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginAdmin login, bool saveMe)
        {
            try
            {
                var Result = client.GetAsync("admin/getloginadmin/" + login.Phone + "/" + login.Password).Result;
                var admin = Result.Content.ReadAsAsync<Admin>().Result;
                
                if(admin.FirstTime == true) return RedirectToAction("FirstLogin" , new {id = admin.Id , name = admin.Name , phone = admin.Phone});
                
                if (admin != null)
                {

                    //if (saveme == true)
                    //{
                    //    HttpCookie co = new HttpCookie("logindata");
                    //    co.Values.Add("id", admin.Id.ToString());
                    //    co.Values.Add("name", admin.Name);
                    //    co.Values.Add("Admin", admin.ToString());

                    //    co.Expires = DateTime.Now.AddDays(1);

                    //    Response.Cookies.Add(co);
                    //}

                    HttpContext.Session.SetInt32("AdminId", admin.Id);

                    return RedirectToAction("view", "admin");
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                ViewBag.msg = "Phone or Password Not Correct";
                return View();
            }
        }


        public ActionResult FirstLogin(int id , string name , string phone) 
        { 
            return View();
        }

        [HttpPost]
        public ActionResult FirstLogin(Admin admin) 
        {
            try
            {
                admin.FirstTime = false;
                admin.AdminDegree = "A";
                var result = client.PutAsJsonAsync("Admin/UpdateAdmin/" + admin.Id, admin).Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("login");
                }
                else
                {
                    return View("Error");
                }
            }
            catch
            {
                return RedirectToAction("login");
            }
        }

        public ActionResult Logout()
        {
            HttpContext.Session.SetInt32("AdminId", 0);

            return RedirectToAction("login");
        }
    }

}
