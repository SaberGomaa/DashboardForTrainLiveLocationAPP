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
            client.BaseAddress = new Uri("http://trainlocationapi-001-site1.atempurl.com/api/");
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
                    HttpContext.Session.SetString("AdminImage", admin.image);

                    HttpContext.Session.GetString("aa");

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
        public async Task<ActionResult> FirstLogin(AdminCreate admin , int id) 
        {
            admin.FirstTime = false;
            admin.AdminDegree = "A";
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
         
            var response = await client.PutAsync("http://trainlocationapi-001-site1.atempurl.com/api/Admin/UpdateAdmin/"+admin.Id, content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("login");
            }
            else
            {
                return View("Error");
            }
        }

        public ActionResult Logout()
        {
            HttpContext.Session.SetInt32("AdminId", 0);

            return RedirectToAction("login");
        }
    }

}
