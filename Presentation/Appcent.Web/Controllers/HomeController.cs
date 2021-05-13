using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

using Appcent.Api.Models.ApplicationUserModels;
using Appcent.Web.ApiServices;

using AutoMapper;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Appcent.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ApplicationUserApiService _applicationUserApiService;
        private readonly IMapper _mapper;

        public HomeController(ApplicationUserApiService categoryApiService, IMapper mapper)
        {
            _applicationUserApiService = categoryApiService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Login");

            //var applicationUsers = await _applicationUserApiService.GetAllAsync();

            //return View(_mapper.Map<IEnumerable<ApplicationUserModel>>(applicationUsers));
        }

        public IActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Insert(ApplicationUserModel model)
        {
            await _applicationUserApiService.AddAsync(model);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            var applicationUser = await _applicationUserApiService.GetByIdAsync(id);
            return View(applicationUser);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ApplicationUserModel model)
        {
            await _applicationUserApiService.UpdateAsync(model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _applicationUserApiService.Remove(id);
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var response = await _applicationUserApiService.LoginAsync(model);

            if (response.ApplicationUser != null && response.Token != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, response.ApplicationUser.Username),
                };

                var claimsIdentity = new ClaimsIdentity(claims, "Login");

                Response.Cookies.Append("token", response.Token);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return Redirect("/Job/Index");
            }
            else
                return View();
        }
    }
}
