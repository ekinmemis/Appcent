using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Appcent.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {

    }
}