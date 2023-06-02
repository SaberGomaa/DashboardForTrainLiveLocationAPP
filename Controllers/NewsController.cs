using Dashboard.Models;
using Microsoft.AspNetCore.Mvc;
using Test.Models;

namespace Dashboard.Controllers
{
    public class NewsController : Controller
    {
        private readonly IWebHostEnvironment host;

        public HttpClient client = new HttpClient();
        public NewsController(IWebHostEnvironment host)
        {
            this.host = host;
            client.BaseAddress = new Uri("http://saberelsayed-001-site1.itempurl.com/api/");
        }
        // GET: News
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Show()
        {
            try
            {
                int? x = HttpContext.Session.GetInt32("AdminId");
                if (x == null)
                {
                    return RedirectToAction("login", "operation");
                }
                var result = client.GetAsync("news/getnews").Result;

                var news = result.Content.ReadAsAsync<List<News>>().Result;

                if (news == null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(news);
                }
            }
            catch
            {
                return View("Error");
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                int? x = HttpContext.Session.GetInt32("AdminId");
                if (x == null)
                {
                    return RedirectToAction("login", "operation");
                }
                var result = client.GetAsync("news/getnewsbyid?Id=" + id).Result;

                var news = result.Content.ReadAsAsync<News>().Result;

                if (news == null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(news);
                }
            }
            catch
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, updateNews news, IFormFile photo)
        {
            try
            {
                if (photo != null)
                {
                    string attach = Path.Combine(host.WebRootPath, "Attach");
                    string fileName = photo.FileName;
                    string fullPath = Path.Combine(attach, fileName);
                    photo.CopyTo(new FileStream(fullPath, FileMode.Create));
                }

                news.Img = photo.FileName;

                var result = client.PutAsJsonAsync("news/updatenews?NewsId=" + id, news).Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Show");
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

        public ActionResult Create()
        {
            int? x = HttpContext.Session.GetInt32("AdminId");
            if (x == null)
            {
                return RedirectToAction("login", "operation");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Create(updateNews news, IFormFile photo)
        {
            try
            {
                if (photo != null)
                {
                    string attach = Path.Combine(host.WebRootPath, "Attach");
                    string fileName = photo.FileName;
                    string fullPath = Path.Combine(attach, fileName);
                    photo.CopyTo(new FileStream(fullPath, FileMode.Create));
                }

                news.Img = photo.FileName;

                var result = client.PostAsJsonAsync("news/createnews", news).Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Show");
                }
                else
                {
                    return View();
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
                var result = client.DeleteAsync("news/deletenews?Id=" + id).Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Show");
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
