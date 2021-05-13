using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Appcent.Api.Controllers
{
    [ApiController]
    [Authorize]
    public class BaseApiController : ControllerBase
    {
    }
}