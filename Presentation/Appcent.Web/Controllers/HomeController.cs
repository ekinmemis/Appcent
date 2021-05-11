using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
