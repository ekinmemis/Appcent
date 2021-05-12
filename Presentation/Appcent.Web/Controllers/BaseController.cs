using Microsoft.AspNetCore.Mvc;

namespace Appcent.Web.Controllers
{
    public class BaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}