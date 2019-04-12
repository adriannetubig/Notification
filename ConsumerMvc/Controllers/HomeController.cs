using System.Web.Mvc;

namespace ConsumerMvc.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Unauthenticated()
        {
            return View();
        }
        public ActionResult Authenticated()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
    }
}