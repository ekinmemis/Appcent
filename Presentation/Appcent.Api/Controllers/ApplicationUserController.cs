using System.Collections.Generic;

using Appcent.Api.Filters;
using Appcent.Api.Models.ApplicationUserModels;
using Appcent.Core.Domain;
using Appcent.Services.ApplicationUserService;

using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Appcent.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApplicationUserController : BaseApiController
    {
        private readonly IApplicationUserService _applicationUserService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public ApplicationUserController(IApplicationUserService applicationUserService, IMapper mapper, IConfiguration configuration, IConfiguration config)
        {
            _applicationUserService = applicationUserService;
            _mapper = mapper;
            _config = config;
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginRequestModel model)
        {
            var applicationUser = _applicationUserService.GetApplicationUserByUsernameAndPassword(model.Username, model.Password);

            var token = _applicationUserService.GenerateJwtToken(applicationUser, _config.GetSection("Secret").ToString());

            return Ok(token);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_mapper.Map<IEnumerable<ApplicationUserModel>>(_applicationUserService.GetAll()));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_mapper.Map<ApplicationUserModel>(_applicationUserService.GetApplicationUserById(id)));
        }

        [ValidationFilter]
        [HttpPost]
        public IActionResult Insert(ApplicationUserModel model)
        {
            _applicationUserService.InsertApplicationUser(_mapper.Map<ApplicationUser>(model));
            return Ok();
        }

        [ValidationFilter]
        [HttpPut]
        public IActionResult Update(ApplicationUserModel model)
        {
            _applicationUserService.UpdateApplicationUser(_mapper.Map<ApplicationUser>(model));
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _applicationUserService.DeleteApplicationUser(_applicationUserService.GetApplicationUserById(id));
            return NoContent();
        }
    }
}