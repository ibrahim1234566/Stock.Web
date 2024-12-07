using Microsoft.AspNetCore.Mvc;

namespace Stock.Web.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
