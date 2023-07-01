using Dashboard.Models;
using Microsoft.AspNetCore.Http;
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

           

            if (Request.Cookies["id"] != null)
            {
                string id = Request.Cookies["id"];
                string name = Request.Cookies["name"];
                string image = Request.Cookies["image"];

                HttpContext.Session.SetInt32("AdminId", int.Parse(id));
                HttpContext.Session.SetString("AdminImage", image);
                HttpContext.Session.SetString("AdminName", name);

                return RedirectToAction("view", "admin");

            }

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

                    if (saveMe == true)
                    {
                        var options = new CookieOptions
                        {
                            Expires = DateTime.Now.AddDays(10)
                        };

                        Response.Cookies.Append("id", admin.Id.ToString(), options);
                        Response.Cookies.Append("name", admin.Name, options);
                        Response.Cookies.Append("image", admin.image, options);

                    }

                    HttpContext.Session.SetInt32("AdminId", admin.Id);
                    HttpContext.Session.SetString("AdminImage", admin.image);
                    HttpContext.Session.SetString("AdminName", admin.Name);

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

            var options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(-11)
            };

            Response.Cookies.Delete("id", options);
            Response.Cookies.Delete("name", options);
            Response.Cookies.Delete("image", options);

            return RedirectToAction("login");
        }
    }

}
