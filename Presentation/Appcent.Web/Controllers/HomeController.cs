using Microsoft.AspNetCore.Mvc;

namespace Appcent.Web.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Test()
        {
            return View();
        }
    }
}