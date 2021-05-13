using System.Collections.Generic;
using System.Threading.Tasks;

using Appcent.Api.Models.Jobs;
using Appcent.Web.ApiServices;

using AutoMapper;

using Microsoft.AspNetCore.Mvc;

namespace Appcent.Web.Controllers
{
    public class JobController : BaseController
    {
        private readonly JobApiService _jobApiService;
        private readonly IMapper _mapper;

        public JobController(JobApiService categoryApiService, IMapper mapper)
        {
            _jobApiService = categoryApiService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var jobs = await _jobApiService.GetAllAsync();
            return View(_mapper.Map<IEnumerable<JobModel>>(jobs));
        }

        public IActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Insert(JobModel model)
        {
            await _jobApiService.AddAsync(model);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            var job = await _jobApiService.GetByIdAsync(id);
            return View(job);
        }

        [HttpPost]
        public async Task<IActionResult> Update(JobModel model)
        {
            await _jobApiService.UpdateAsync(model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _jobApiService.Remove(id);
            return RedirectToAction("Index");
        }
    }
}
