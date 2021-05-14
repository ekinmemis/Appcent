using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Appcent.Web.Helpers
{
    public class CookieMiddleware
    {
        private readonly RequestDelegate _next;

        public CookieMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var accessToken = await context.GetTokenAsync("access_token");

            

            await _next(context);
        }
    }
}
