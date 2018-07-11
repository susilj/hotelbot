
namespace Susil.Tutorial.Bots.Microsoft.HotelBot.Controllers
{
    using System.Web.Mvc;

    public class Chat
    {
        public string ChatMessage { get; set; }
        public string ChatResponse { get; set; }
        public string watermark { get; set; }
    }

    [Route("[controller]")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}