using System.Linq;
using System.Threading.Tasks;

using Appcent.Services.ApplicationUsers;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Appcent.Api.Helpers
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _config;
        private readonly IApplicationUserService _applicationUserService;

        public JwtMiddleware(RequestDelegate next, IConfiguration config, IApplicationUserService applicationUserService)
        {
            _next = next;
            _config = config;
            _applicationUserService = applicationUserService;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                _applicationUserService.AttachUserToContext(context, token);

            await _next(context);
        }
    }
}