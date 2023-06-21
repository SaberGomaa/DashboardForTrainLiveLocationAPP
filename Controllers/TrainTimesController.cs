using Microsoft.AspNetCore.Mvc;

namespace Dashboard.Controllers
{
    public class TrainTimesController : Controller
    {
        public HttpClient client = new HttpClient();
        public TrainTimesController()
        {
            client.BaseAddress = new Uri("http://trainlocationapi-001-site1.atempurl.com/api/");
        }
    }
}
